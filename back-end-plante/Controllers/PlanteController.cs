using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanteController : BaseController
{
    private readonly IPlanteRepository _planteRepository;

    public PlanteController(IHttpContextAccessor httpContextAccessor, IPlanteRepository planteRepository) : base(httpContextAccessor)
    {
        _planteRepository = planteRepository;
    }
    
    /// <summary>
    /// Get all plants
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<List<Plant>> GetPlants()
    {
        if (!IsAdmin())
            throw new UnauthorizedAccessException("you aren't admin");

        return await _planteRepository.GetPlantsAsync();
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
    
    //TODO faire une route de getPlante by listId
    [Authorize]
    [HttpGet("ids")]
    public Task<List<Plant>> GetPlants([FromQuery] List<string> plantIds)
    {
        return null;
    }

    /// <summary>
    /// Get all plants by user id 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("user")]
    public Task<List<Plant>> GetPlantsByUserId()
    {
        var userId = GetUserId();
        return _planteRepository.GetPlantsByUserId(userId);
    }
    
    /// <summary>
    /// Add a plant 
    /// </summary>
    /// <param name="plantRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut]
    public Task<Plant> CreatePlant([FromBody] PlantRequest plantRequest)
    {
        var userId = GetUserId();
        plantRequest.UserId = userId;
        return _planteRepository.AddPlant(plantRequest.toPlant());
    }
    
    /// <summary>
    /// update a plant
    /// </summary>
    /// <param name="plant"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public Task<Plant> ModifyPlant([FromBody] Plant plant)
    {
        var userId = GetUserId();
        plant.UserId = userId;
        
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
        if (!IsAdmin())
            throw new UnauthorizedAccessException("you aren't admin");
        
        return _planteRepository.DeletePlant(id);
    }
    
    /// <summary>
    /// delete a plant by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("deletePlant/{id}/user")]
    public Task<bool> DeletePlantByUserId([FromRoute] string id)
    {
        return _planteRepository.DeletePlant(id, GetUserId());
    }
}