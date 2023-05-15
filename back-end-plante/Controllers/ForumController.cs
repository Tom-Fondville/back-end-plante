using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;


[ApiController]
[Route("[controller]")]
public class ForumController : ControllerBase
{
    private readonly IForumService _forumService;

    public ForumController(IForumService forumService)
    {
        _forumService = forumService;
    }

    [Authorize]
    [HttpGet]
    public async Task<List<Forum>> GetForums()
    {
        return await _forumService.GetForums();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<Forum> GetForum([FromRoute] string id)
    {
        return await _forumService.GetForumById(id);
    }

    [Authorize]
    [HttpPost("Question")]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
    {
        await _forumService.CreateQuestion(request);
        return NoContent();
    }

    [Authorize]
    [HttpPost("Response")]
    public async Task<IActionResult> AddResponse([FromBody] AddReponseRequest request)
    {
        await _forumService.AddResponse(request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteForum([FromRoute] string id)
    {
        await _forumService.DeleteForum(id);
        return NoContent();
    }
    
}