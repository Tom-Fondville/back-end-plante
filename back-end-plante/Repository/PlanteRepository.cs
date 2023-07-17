using back_end_plante.Common.Models;
using back_end_plante.Configurations;
using back_end_plante.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end_plante.Repository;

public class PlanteRepositry : MsprPlanteRepositoryBase, IPlanteRepository
{
    private readonly IMongoCollection<Plant> _plantCollection;
    
    public PlanteRepositry(IOptions<ConnectionStringConfiguration> connectionStringConfiguration) : base(connectionStringConfiguration)
    {
        try
        {
            var db = GetClient().WithReadPreference(ReadPreference.Secondary).GetDatabase(MongoPocProviderName);
            _plantCollection = db.GetCollection<Plant>("Plant");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Plant>> GetPlantsAsync()
    {
        var builder = Builders<Plant>.Filter;
        var filter = builder.Empty;
        
        var result = await _plantCollection.FindAsync(filter);

        return result.ToList();
    }

    public async Task<List<Plant>> GetPlantsByIdsAsync(List<string> plantIds)
    {
        var builder = Builders<Plant>.Filter;
        var filter = builder.In(p => p.Id, plantIds);

        var result = await _plantCollection.FindAsync(filter);
        return result.ToList();
    }

    public async Task<Plant> GetPlantById(string id)
    {
        var builder = Builders<Plant>.Filter;
    
        var filter = builder.Eq(p => p.Id, id);
        
        var result = await _plantCollection.FindAsync(filter);
        
        return result.SingleOrDefault();
    }

    public async Task<List<Plant>> GetPlantsByUserId(string id)
    {
        var builder = Builders<Plant>.Filter;
        
        var filter = builder.Eq(p => p.UserId, id);
        
        var result = await _plantCollection.FindAsync(filter);

        return await result.ToListAsync();
        
    }

    public async Task<Plant> AddPlant(Plant plant)
    {
        await _plantCollection.InsertOneAsync(plant);
        return await _plantCollection.Find(p => p.Id == plant.Id).FirstOrDefaultAsync();
    }

    public async Task<Plant> ModifyPlant(Plant newPlant)
    {
        var builder = Builders<Plant>.Filter;
        
        var filter = builder.Eq(p => p.Id, newPlant.Id);
        
        var result = await _plantCollection.ReplaceOneAsync(filter, newPlant);

        if (result.ModifiedCount == 0)
        {
            throw new Exception($"Failed to update plant with id {newPlant.Id}");
        }

        return newPlant;
    }

    public async Task<bool> DeletePlant(string id)
    {
        var builder = Builders<Plant>.Filter;
        var filter = builder.Eq(p => p.Id, id);
        
        var result = await _plantCollection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }

    public async Task<bool> DeletePlant(string id, string userId)
    {
        var filter = Builders<Plant>.Filter.Eq(p => p.Id, id)
                     & Builders<Plant>.Filter.Eq(p => p.UserId, userId);

        var result = await _plantCollection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }
}