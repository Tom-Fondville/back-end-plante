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
        var token = await _userService.UserLogin(loginRequest);
        return Ok(token);
    }
    
    /// <summary>
    /// login and give you a token for admin
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login/admin")]
    public async Task<IActionResult> AdminLogin([FromBody] LoginRequest loginRequest)
    {
        var token = await _userService.AdminLogin(loginRequest);
        return Ok(token);
    }

    /// <summary>
    /// register
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPut]
    public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
    {
        await _userService.Register(userRequest);
        return NoContent();
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        if (!IsAdmin())
            return Unauthorized("you aren't admin");

        var response = await _userService.GetUsers();
        return Ok(response);
    }
    
    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<User> GetUserById([FromRoute] string id)
    {
        if (!IsAdmin())
            id = GetUserId();
        
        return await _userService.GetUserById(id);
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
        if (!IsAdmin())
            userRequest.Id = GetUserId();
        
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
            id = GetUserId();
        
        await _userService.DeleteUserById(id);
        return NoContent();
    }
}