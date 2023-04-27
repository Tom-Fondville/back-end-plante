using back_end_plante.Common.Models;
using back_end_plante.Configurations;
using back_end_plante.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace back_end_plante.Repository;

public class UserRepository : MsprPlanteRepositoryBase, IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;
    public UserRepository(IOptions<ConnectionStringConfiguration> connectionStringConfiguration) : base(connectionStringConfiguration)
    {
        try
        {
            var db = GetClient().WithReadPreference(ReadPreference.Secondary).GetDatabase(MongoPocProviderName);
            _userCollection = db.GetCollection<User>("User");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<User> GetUserById(string id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        var response = await _userCollection.FindAsync(filter);
        
        var user = response.ToList().FirstOrDefault();
        if (user is null) throw new BadHttpRequestException("User not found");

        return user;
    }
    
    public async Task<User> GetUserByMailAndPassword(string mail, string password)
    {
        var filter = Builders<User>.Filter.Where(user => user.Mail == mail && user.Password == password);
        var response = await _userCollection.FindAsync(filter);
        
        var user = response.ToList().FirstOrDefault();
        if (user is null) throw new BadHttpRequestException("User not found");

        return user;
    }

    public async Task UpdateUser(string userId, User user)
    {
        var builder = Builders<User>.Filter;

        var filter = builder.Eq(p => p.Id, user.Id);

        var result = await _userCollection.ReplaceOneAsync(filter, user);

        if (result.ModifiedCount == 0)
        {
            throw new BadHttpRequestException($"Failed to update plant with id {user.Id}");
        }
    }
    public Task DeleteUserById(string id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        return _userCollection.DeleteOneAsync(filter);
    }
    
    public async Task Register(User user)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Mail, user.Mail);
        var response = await _userCollection.FindAsync(filter);
        if (response.ToList().Any())
            throw new BadHttpRequestException("User already exist");
        
        await _userCollection.InsertOneAsync(user);
        // var filter = Builders<User>.Filter.Eq(u => u.Mail, user.Mail);
        // var update = Builders<User>.Update.SetOnInsert(u => u, user);
        //    var option = new UpdateOptions { IsUpsert = true };

        // return _userCollection.UpdateOneAsync(filter, update, option);
    }
}