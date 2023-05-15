using back_end_plante.Common.Enums;
using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Repository.Interfaces;
using back_end_plante.Service.Interfaces;

namespace back_end_plante.Service;

public class AnnonceService : IAnnonceService
{
    private readonly IAnnonceRepository _annonceRepository;

    public AnnonceService(IAnnonceRepository annonceRepository)
    {
        _annonceRepository = annonceRepository;
    }

    public Task CreateAnnonce(AnnonceRequest request)
    {
        return _annonceRepository.CreateAnnonce(request.ToAnnonce());
    }

    public Task AddPossibleGarden(string annonceId, string possibleGardenId)
    {
        return _annonceRepository.AddPossibleGarden(annonceId, possibleGardenId);
    }

    public Task DeleteAnnonce(string annonceId)
    {
        return _annonceRepository.DeleteAnnonce(annonceId);
    }

    public Task<Annonce> GetAnnonceById(string annonceId)
    {
        return _annonceRepository.GetAnnonceById(annonceId);
    }
    
    public async Task ValidateGarden(string annonceId, string gardenId)
    {
        var annonce = await _annonceRepository.GetAnnonceById(annonceId);
        annonce.Status = AnnonceStatus.GardenFound;
        annonce.PossiblesGardiensId = new List<string>{ gardenId };
        await _annonceRepository.UpdateAnnonce(annonceId, annonce);
    }

}