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
    private readonly IUserRepository _userRepository;

    public DiscussionController(IHttpContextAccessor httpContextAccessor, IDiscussionRepository discussionRepository, IUserRepository userRepository) : base(httpContextAccessor)
    {
        _discussionRepository = discussionRepository;
        _userRepository = userRepository;
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
    /// <param name="userId1"></param>
    /// <param name="userId2"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<Discussion> GetMessagesByDiscution(string userId1, string userId2)
    {
        var userId = GetUserId();
        var discussionId = new DiscussionId { UserId1 = userId1, UserId2 = userId2 };
        var response = await _discussionRepository.GetMessagesByDiscution(discussionId, userId);
    
        return response;
    }
    
    /// <summary>
    /// Create a discussion with a userId from userId
    /// </summary>
    /// <param name="discussion"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateDiscussion(string userId)
    {

        var user1 = await _userRepository.GetUserById(GetUserId());
        var user2 = await _userRepository.GetUserById(userId);

        var discussion = new Discussion
        {
            Id = new DiscussionId
            {
                UserId1 = user1.Id,
                UserId2 = user2.Id
            },
            LastUpdate = DateTime.UtcNow,
            Messages = new List<Message>(),
            UserName = $"{user2.Name} {user2.SurName}"
        };

        await _discussionRepository.CreateDiscution(discussion);
        
        return NoContent();
    }

    /// <summary>
    /// Send a message into a discussion
    /// </summary>
    /// <param name="request"></param>
    [Authorize]
    [HttpPut]
    public async Task SendMessage([FromBody] SendMessageRequest request)
    {
        var user = await _userRepository.GetUserById(GetUserId());
        request.Message.UserName = user.SurName;
        request.Message.Date = DateTime.UtcNow;
        
        await _discussionRepository.SendMessage(request, user.Id);
    }
    
    /// <summary>
    /// Delete a discussion
    /// </summary>
    /// <param name="discussionId"></param>
    /// <returns></returns>
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