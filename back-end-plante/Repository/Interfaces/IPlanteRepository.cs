using back_end_plante.Common.Models;

namespace back_end_plante.Repository.Interfaces;

public interface IPlanteRepository
{
    Task<List<Plant>> GetPlantsAsync();
}