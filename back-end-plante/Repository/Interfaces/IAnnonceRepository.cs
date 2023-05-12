using back_end_plante.Common.Models;

namespace back_end_plante.Repository.Interfaces;

public interface IAnnonceRepository
{
    public Task<Annonce> GetAnnonceById(string annonceId);
    public Task CreateAnnonce(Annonce request);
    public Task AddPossibleGarden(string annonceId, string possibleGardenId);
    public Task DeleteAnnonce(string annonceId);
    public Task UpdateAnnonce(string annonceId, Annonce annonce);
}