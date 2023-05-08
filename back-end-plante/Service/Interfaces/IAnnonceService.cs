using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;

namespace back_end_plante.Service.Interfaces;

public interface IAnnonceService
{
    Task<Annonce> GetAnnonceById(string annonceId);
    Task CreateAnnonce(AnnonceRequest request);
    public Task AddPossibleGarden(string annonceId, string possibleGardenId);
    public Task DeleteAnnonce(string annonceId);
}