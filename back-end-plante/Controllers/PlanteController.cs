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
    
    /// <summary>
    /// Get all plants
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("All")]
    public Task<List<Plant>> Get()
    {
        return _planteRepository.GetPlantsAsync();
    }
    
    /// <summary>
    /// Get a plant by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    public Task<Plant> GetPlantById([FromRoute] string id)
    {
        return _planteRepository.GetPlantById(id);
    }
    
    /// <summary>
    /// Get all plants by user id 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("user/{userId}")]
    public Task<List<Plant>> GetPlantsByUserId([FromRoute] string userId)
    {
        return _planteRepository.GetPlantsByUserId(userId);
    }
    
    /// <summary>
    /// Add a plant 
    /// </summary>
    /// <param name="plantRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("addPlant")]
    public Task<Plant> AddPlant([FromBody] PlantRequest plantRequest)
    {
        return _planteRepository.AddPlant(plantRequest.toPlant());
        
    }
    
    /// <summary>
    /// update a plant
    /// </summary>
    /// <param name="plant"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("modifyPlant")]
    public Task<Plant> ModifyPlant([FromBody] Plant plant)
    {
        return _planteRepository.ModifyPlant(plant);
    }

    /// <summary>
    /// delete a plant by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("deletePlant/{id}")]
    public Task<bool> DeletePlant([FromRoute] string id)
    {
        return _planteRepository.DeletePlant(id);
    }
}