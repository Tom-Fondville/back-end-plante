using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class AnnonceController : ControllerBase
{

    private readonly IAnnonceService _annonceService;

    public AnnonceController(IAnnonceService annonceService)
    {
        _annonceService = annonceService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<Annonce> GetAnnonceById([FromRoute] string id)
    {
        return await _annonceService.GetAnnonceById(id);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAnnonce([FromBody] AnnonceRequest request)
    {
        await _annonceService.CreateAnnonce(request);
        return NoContent();
    }
    
    [Authorize]
    [HttpPost("{id}")]
    public async Task<IActionResult> AddPossibleGardenId([FromRoute] string id, [FromQuery] string possibleGardenId)
    {
        await _annonceService.AddPossibleGarden(id,possibleGardenId);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnnonce([FromRoute] string id)
    {
        await _annonceService.DeleteAnnonce(id);
        return NoContent();
    }

}