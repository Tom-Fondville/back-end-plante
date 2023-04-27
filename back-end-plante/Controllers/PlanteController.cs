using back_end_plante.Common.Models;
using back_end_plante.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanteController
{

    private readonly IPlanteRepository _planteRepository;

    public PlanteController(IPlanteRepository planteRepository)
    {
        _planteRepository = planteRepository;
    }
    
    [AllowAnonymous]
    [HttpGet("All")]
    public Task<List<Plant>> Get()
    {
        return _planteRepository.GetPlantsAsync();
    }

}