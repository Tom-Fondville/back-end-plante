using back_end_plante.Common.Models;

namespace back_end_plante.Repository.Interfaces;

public interface IPlanteRepository
{
    Task<List<Plant>> GetPlantsAsync();

    Task<List<Plant>> GetPlantsByIdsAsync(List<string> plantIds);
    Task<Plant> GetPlantById(string id);
    
    Task<List<Plant>> GetPlantsByUserId(string id);


    Task<Plant> AddPlant(Plant newPlant);

    Task<Plant> ModifyPlant(Plant newPlant);

    Task<bool> DeletePlant(string id);
    Task<bool> DeletePlant(string id, string userId);

}