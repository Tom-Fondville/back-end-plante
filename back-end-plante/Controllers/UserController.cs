using System.IdentityModel.Tokens.Jwt;
using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Common.Requests.user;
using back_end_plante.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
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
    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
    {
        var token = await _userService.Login(email, password);
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }

    /// <summary>
    /// register
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
    {
        await _userService.Register(userRequest);
        return NoContent();
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
        return await _userService.GetUserById(id);
    }
    
    /// <summary>
    /// update a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateUserById([FromRoute] string id, [FromBody] UserRequest userRequest)
    {
        await _userService.UpdateUser(id, userRequest);
        return NoContent();
    }

    /// <summary>
    /// add a adresse 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="adresses"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Address")]
    public async Task<IActionResult> AddAddress([FromQuery] string userId, [FromBody] List<Adress> adresses)
    {
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
        await _userService.DeleteUserById(id);
        return NoContent();
    }
}