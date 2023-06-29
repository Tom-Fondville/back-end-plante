using back_end_plante.Common.Models;
using back_end_plante.Common.Requests.user;
using back_end_plante.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _userService = userService;
    }

    /// <summary>
    /// login and give you a token 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var token = await _userService.Login(loginRequest);
        return Ok(token);
    }

    /// <summary>
    /// register
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPut("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
    {
        await _userService.Register(userRequest);
        return NoContent();
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    public async Task<List<User>> GetUsers()
    {
        if (!IsAdmin())
            throw new UnauthorizedAccessException("you aren't admin");

        return await _userService.GetUsers();
    }
    
    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("id")]
    public async Task<User> GetUserById()
    {
        var userId = GetUserId();
        return await _userService.GetUserById(userId);
    }
    
    /// <summary>
    /// update a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpdateUserById([FromBody] User userRequest)
    {
        var userId = GetUserId();
        userRequest.Id = userId;
        
        await _userService.UpdateUser(userRequest);
        return NoContent();
    }
    
    /// <summary>
    /// update a user from admin
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("admin")]
    public async Task<IActionResult> UpdateUserByIdFromAdmin([FromBody] User userRequest)
    {
        if (!IsAdmin())
            return Unauthorized("You'r not admin");

        await _userService.UpdateUser(userRequest);
        return NoContent();
    }

    /// <summary>
    /// add a adresse 
    /// </summary>
    /// <param name="adresses"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Address")]
    public async Task<IActionResult> AddAddress([FromBody] List<Adress> adresses)
    {
        var userId = GetUserId();
        await _userService.AddAdresse(userId, adresses);
        return NoContent();
    }

    /// <summary>
    /// delete a user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserById(string id)
    {
        if (!IsAdmin())
            throw new UnauthorizedAccessException("you aren't admin");
        
        await _userService.DeleteUserById(id);
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserById()
    {
        var userId = GetUserId();
        await _userService.DeleteUserById(userId);
        return NoContent();
    }
}