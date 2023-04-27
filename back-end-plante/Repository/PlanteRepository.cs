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
}