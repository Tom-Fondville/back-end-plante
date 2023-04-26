using back_end_plante.Models;

namespace back_end_plante.Repository.Interfaces;

public interface IPlanteRepository
{
    Task<List<Plante>> GetPlantesAsync();
}