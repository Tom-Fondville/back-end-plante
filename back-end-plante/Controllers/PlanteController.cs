using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
    
    [Authorize]
    [HttpGet("All")]
    public Task<List<Plant>> Get()
    {
        return _planteRepository.GetPlantsAsync();
    }
    
    
    [Authorize]
    [HttpGet("{id}")]
    public Task<Plant> GetPlantById([FromRoute] string id)
    {
        return _planteRepository.GetPlantById(id);
    }
    
    [Authorize]
    [HttpGet("user/{userId}")]
    public Task<List<Plant>> GetPlantsByUserId([FromRoute] string userId)
    {
        return _planteRepository.GetPlantsByUserId(userId);
    }
    
    
    [Authorize]
    [HttpPost("addPlant")]
    public Task<Plant> AddPlant([FromBody] PlantRequest plantRequest)
    {
        return _planteRepository.AddPlant(plantRequest.toPlant());
        
    }
    
    [Authorize]
    [HttpPut("modifyPlant")]
    public Task<Plant> ModifyPlant([FromBody] Plant plant)
    {
        return _planteRepository.ModifyPlant(plant);
    }

    [Authorize]
    [HttpDelete("deletePlant/{id}")]
    public Task<bool> DeletePlant([FromRoute] string id)
    {
        return _planteRepository.DeletePlant(id);
    }
    

}