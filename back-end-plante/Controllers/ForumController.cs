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

    /// <summary>
    /// Get all forum messages
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<List<Forum>> GetForums()
    {
        return await _forumService.GetForums();
    }

    /// <summary>
    /// Get a forum message by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<Forum> GetForum([FromRoute] string id)
    {
        return await _forumService.GetForumById(id);
    }

    /// <summary>
    /// Post a question in the forum
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Question")]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
    {
        await _forumService.CreateQuestion(request);
        return NoContent();
    }

    /// <summary>
    /// Post a response to a question in the forum
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Response")]
    public async Task<IActionResult> AddResponse([FromBody] AddReponseRequest request)
    {
        await _forumService.AddResponse(request);
        return NoContent();
    }

    /// <summary>
    /// Update a forum question or/and response
    /// </summary>
    /// <param name="forum"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Forum")]
    public async Task<IActionResult> UpdateForum([FromBody] Forum forum)
    {
        await _forumService.UpdateForum(forum);
        return NoContent();
    }

    /// <summary>
    /// Delete a forum message 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteForum([FromRoute] string id)
    {
        await _forumService.DeleteForum(id);
        return NoContent();
    }
    
}