using back_end_plante.Configurations;
using back_end_plante.Models;
using back_end_plante.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end_plante.Repository;

public class PlanteRepositry : MsprPlanteRepositoryBase, IPlanteRepository
{
    private readonly IMongoCollection<Plante> _planteCollection;
    
    public PlanteRepositry(IOptions<ConnectionStringConfiguration> connectionStringConfiguration) : base(connectionStringConfiguration)
    {
        try
        {
            var db = GetClient().WithReadPreference(ReadPreference.Secondary).GetDatabase(MongoPocProviderName);
            _planteCollection = db.GetCollection<Plante>("Plante");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Plante>> GetPlantesAsync()
    {
        var builder = Builders<Plante>.Filter;
        var filter = builder.Empty;
        
        var result = await _planteCollection.FindAsync(filter);

        return result.ToList();
    }
}