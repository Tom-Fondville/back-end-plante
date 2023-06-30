using back_end_plante.Common.Models;
using back_end_plante.Configurations;
using back_end_plante.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end_plante.Repository;

public class UserRepository : MsprPlanteRepositoryBase, IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;
    private FindOptions<User> _findOptions;
    public UserRepository(IOptions<ConnectionStringConfiguration> connectionStringConfiguration) : base(connectionStringConfiguration)
    {
        try
        {
            var db = GetClient().WithReadPreference(ReadPreference.Secondary).GetDatabase(MongoPocProviderName);
            _userCollection = db.GetCollection<User>("User");
            
            _findOptions = new FindOptions<User>
            {
                Projection = Builders<User>.Projection.Exclude(user => user.Password)
            };
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

    public async Task<List<User>> GetUsers()
    {
        var filter = Builders<User>.Filter.Empty;

        var response = await _userCollection.FindAsync(filter, _findOptions);

        return response.ToList();
    }
    
    public async Task<User> GetUserByMailAndPassword(string mail, string password, bool isForAdmin = false)
    {
        var filter = Builders<User>.Filter.Where(user => user.Mail == mail && user.Password == password);

        if (isForAdmin)
            filter &= Builders<User>.Filter.Eq(user => user.IsAdmin, true);

        
        var response = await _userCollection.FindAsync(filter, _findOptions);
        
        var user = response.ToList().FirstOrDefault();
        if (user is null) throw new BadHttpRequestException("User not found");

        return user;
    }

    public async Task UpdateUser(User user)
    {
        var builder = Builders<User>.Filter;

        var filter = builder.Eq(p => p.Id, user.Id);

        var result = await _userCollection.ReplaceOneAsync(filter, user);

        if (result.ModifiedCount == 0)
        {
            throw new BadHttpRequestException($"Failed to update user with id {user.Id}");
        }
    }

    public async Task AddAdresse(string userId, List<Adress> adresses)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var updates = Builders<User>.Update.PushEach(u => u.Adresses, adresses);

        await _userCollection.UpdateOneAsync(filter, updates);
    }
    
    public Task DeleteUserById(string id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        return _userCollection.DeleteOneAsync(filter);
    }
    
    public async Task Register(User user)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Mail, user.Mail);
        var response = await _userCollection.FindAsync(filter, _findOptions);
        if (response.ToList().Any())
            throw new BadHttpRequestException("User already exist");
        
        await _userCollection.InsertOneAsync(user);
    }
}