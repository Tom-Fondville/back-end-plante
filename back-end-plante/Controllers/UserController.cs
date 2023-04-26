using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using back_end_plante.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace back_end_plante.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUtilisateurService _utilisateurService;

    public UserController(IConfiguration configuration, IUtilisateurService utilisateurService)
    {
        _configuration = configuration;
        _utilisateurService = utilisateurService;
    }


    [HttpGet("login")]
    public IActionResult Login([FromQuery] string email, [FromQuery] string password)
    {
        var token = GenerateToken("tom");
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }
    
    private JwtSecurityToken GenerateToken(string mail)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,mail),
        };
        return new JwtSecurityToken
        (
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );
    }
}