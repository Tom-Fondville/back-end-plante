using back_end_plante.Common.Models;
using back_end_plante.Configurations;
using back_end_plante.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end_plante.Repository;

public class AnnonceRepository : MsprPlanteRepositoryBase, IAnnonceRepository
{
    private readonly IMongoCollection<Annonce> _annonceCollection;
    
    public AnnonceRepository(IOptions<ConnectionStringConfiguration> connectionStringConfiguration) : base(connectionStringConfiguration)
    {
        try
        {
            var db = GetClient().WithReadPreference(ReadPreference.Secondary).GetDatabase(MongoPocProviderName);
            _annonceCollection = db.GetCollection<Annonce>("Annonce");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Annonce>> GetAnnonce()
    {
        var filter = Builders<Annonce>.Filter.Empty;
        var response = await _annonceCollection.FindAsync(filter);
        return response.ToList();
    }

    public async Task<Annonce> GetAnnonceById(string annonceId)
    {
        var filter = Builders<Annonce>.Filter.Eq(a => a.Id, annonceId);

        var responce =  await _annonceCollection.FindAsync(filter);
        var annonce = responce.ToList().FirstOrDefault();
        if (annonce is null) throw new BadHttpRequestException("Annonce not found");
        return annonce;
    }
    public async Task CreateAnnonce(Annonce request)
    {
        await _annonceCollection.InsertOneAsync(request);
    }
    
    public async Task AddPossibleGarden(string annonceId, string userId, string possibleGardenId)
    {
        var filter = Builders<Annonce>.Filter.Eq(a => a.Id, annonceId)
            & Builders<Annonce>.Filter.Eq(a => a.UserId, userId);
        var update = Builders<Annonce>.Update.Push(a => a.PossiblesGardiensId, possibleGardenId);

        await _annonceCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAnnonce(string annonceId, string userId)
    {
        var filter = Builders<Annonce>.Filter.Eq(a => a.Id, annonceId)
            & Builders<Annonce>.Filter.Eq(a => a.UserId, userId);

        await _annonceCollection.DeleteOneAsync(filter);
    }

    public async Task UpdateAnnonce(string annonceId, string userId, Annonce annonce)
    {
        var builder = Builders<Annonce>.Filter;

        var filter = builder.Eq(a => a.Id, annonceId)
            & builder.Eq(a => a.UserId, userId);

        var result = await _annonceCollection.ReplaceOneAsync(filter, annonce);

        if (result.ModifiedCount == 0)
        {
            throw new BadHttpRequestException($"Failed to update annonce with id {annonce.Id}");
        }
    }
}