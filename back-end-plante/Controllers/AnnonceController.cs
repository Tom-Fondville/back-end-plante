using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class AnnonceController : BaseController
{
    private readonly IAnnonceService _annonceService;

    public AnnonceController(IAnnonceService annonceService, IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
    {
        _annonceService = annonceService;
    }

    /// <summary>
    /// Get annonce by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<Annonce> GetAnnonceById([FromRoute] string id)
    {
        return await _annonceService.GetAnnonceById(id);
    }
    
    /// <summary>
    /// Get annonce by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<List<Annonce>> GetAnnonce()
    {
        return await _annonceService.GetAnnonce();
    }

    /// <summary>
    /// Create annonce
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> CreateAnnonce([FromBody] AnnonceRequest request)
    {
        var userId = GetUserId();
        request.UserId = userId;
            
        await _annonceService.CreateAnnonce(request);
        return NoContent();
    }
    
    /// <summary>
    /// Add a possible garden
    /// </summary>
    /// <param name="id"></param>
    /// <param name="possibleGardenId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("{id}")]
    public async Task<IActionResult> AddPossibleGardenId([FromRoute] string id, [FromQuery] string possibleGardenId)
    {
        await _annonceService.AddPossibleGarden(id, GetUserId(), possibleGardenId);
        return NoContent();
    }
    
    /// <summary>
    /// delete a annonce by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnnonce([FromRoute] string id)
    {
        var userId = GetUserId();
        await _annonceService.DeleteAnnonce(id, userId);
        return NoContent();
    }
    
    /// <summary>
    /// validate a garden  
    /// </summary>
    /// <param name="id"></param>
    /// <param name="garden"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("{id}/validateGarden")]
    public async Task<IActionResult> ValidateGarden([FromRoute] string id, [FromQuery] string garden)
    {
        await _annonceService.ValidateGarden(id, GetUserId(), garden);
        return NoContent();
    }

}