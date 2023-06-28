using back_end_plante.Common.Models;

namespace back_end_plante.Repository.Interfaces;

public interface IAnnonceRepository
{
    public Task<List<Annonce>> GetAnnonce();
    public Task<Annonce> GetAnnonceById(string annonceId);
    public Task CreateAnnonce(Annonce request);
    public Task AddPossibleGarden(string annonceId, string userId, string possibleGardenId);
    public Task DeleteAnnonce(string annonceId, string userId);
    public Task UpdateAnnonce(string annonceId, string userId, Annonce annonce);
}