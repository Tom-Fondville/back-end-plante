using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using back_end_plante.Models;
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
        var tokenHandler = new JwtSecurityTokenHandler();
        

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "le nom du toto"),
            new("userId", "id du toto"),
            new("userName", "le nom du toto")
        };
        var token = GetToken(claims);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(24),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.Sha256)
        );
        return token;
    }
}