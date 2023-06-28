using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;

namespace back_end_plante.Service.Interfaces;

public interface IAnnonceService
{
    public Task<List<Annonce>> GetAnnonce();
    Task<Annonce> GetAnnonceById(string annonceId);
    Task CreateAnnonce(AnnonceRequest request);
    public Task AddPossibleGarden(string annonceId, string userId, string possibleGardenId);
    public Task DeleteAnnonce(string annonceId, string userId);
    public Task ValidateGarden(string annonceId, string userId, string gardenId);
}