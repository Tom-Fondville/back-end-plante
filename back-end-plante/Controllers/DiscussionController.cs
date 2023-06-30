using back_end_plante.Common.Models.Messaging;
using back_end_plante.Common.Requests;
using back_end_plante.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class DiscussionController : BaseController
{
    private readonly IDiscussionRepository _discussionRepository;

    public DiscussionController(IHttpContextAccessor httpContextAccessor, IDiscussionRepository discussionRepository) : base(httpContextAccessor)
    {
        _discussionRepository = discussionRepository;
    }
    
    /// <summary>
    /// Get all discutions for admin
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetDiscussions()
    {
        if (!IsAdmin())
            return Unauthorized("you'r not admin");
    
        return Ok(await _discussionRepository.GetDiscussions());
    }
    
    /// <summary>
    /// Get all discutions for an user
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("userId")]
    public async Task<IActionResult> GetDiscussionsByUser()
    {
        var userId = GetUserId();
    
        var response = await _discussionRepository.GetDiscussionsByUser(userId);
        return Ok(response);
    }

    /// <summary>
    /// Get all messages from a discussion 
    /// </summary>
    /// <param name="discussionId"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetMessagesByDiscution(DiscussionId discussionId)
    {
        var userId = GetUserId();
        var response = await _discussionRepository.GetMessagesByDiscution(discussionId, userId);
    
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut]
    public async Task SendMessage([FromBody] SendMessageRequest request)
    {
        var userId = GetUserId();
        await _discussionRepository.SendMessage(request, userId);
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteDiscussionById(DiscussionId discussionId)
    {
        var userId = GetUserId();
        var deletedCount = await _discussionRepository.DeleteDiscussionById(discussionId, userId);
    
        if (deletedCount == 0)
            return BadRequest("Discution not deleted");
    
        return Ok();
    }
    
}