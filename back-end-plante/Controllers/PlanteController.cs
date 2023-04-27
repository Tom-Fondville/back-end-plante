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
    
    [AllowAnonymous]
    [HttpGet("All")]
    public Task<List<Plant>> Get()
    {
        return _planteRepository.GetPlantsAsync();
    }
    
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public Task<Plant> GetPlantById([FromRoute] string id)
    {
        return _planteRepository.GetPlantById(id);
    }
    
    [AllowAnonymous]
    [HttpGet("user/{userId}")]
    public Task<List<Plant>> GetPlantsByUserId([FromRoute] string userId)
    {
        return _planteRepository.GetPlantsByUserId(userId);
    }
    
    
    [AllowAnonymous]
    [HttpPost("addPlant")]
    public Task<Plant> AddPlant([FromBody] PlantRequest plantRequest)
    {
        return _planteRepository.AddPlant(plantRequest.toPlant());
        
    }
    
    [AllowAnonymous]
    [HttpPut("modifyPlant")]
    public Task<Plant> ModifyPlant([FromBody] Plant plant)
    {
        return _planteRepository.ModifyPlant(plant);
    }

    [AllowAnonymous]
    [HttpDelete("deletePlant/{id}")]
    public Task<bool> DeletePlant([FromRoute] string id)
    {
        return _planteRepository.DeletePlant(id);
    }
    

}