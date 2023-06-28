using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace back_end_plante.Controllers;

public class BaseController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public BaseController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected (string userId, bool isBotaniste, bool isAdmin) GetDataFromToken()
    {
        StringValues token = new StringValues();
        _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out token);

        var truc = token.ToString().Split(" ");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(truc[1]);
        var userId = securityToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        var isBotaniste = securityToken.Claims.FirstOrDefault(c => c.Type == "IsBotaniste")?.Value;
        var isAdmin = securityToken.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;

        if (userId is null)
            throw new BadHttpRequestException("UserId missing");
        if (isBotaniste is null)
            throw new BadHttpRequestException("isBotaniste missing");
        if (isAdmin is null)
            throw new BadHttpRequestException("isAdmin missing");
        return (userId, isBotaniste == "true", isAdmin == "true");
    }
    
    protected string GetUserId()
    {
        StringValues token = new StringValues();
        _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out token);
        var truc = token.ToString().Split(" ");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(truc[1]);
        var userId = securityToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        
        if (userId is null)
            throw new BadHttpRequestException("UserId missing");

        return userId;
    }
    
    protected bool IsAdmin()
    {
        StringValues token = new StringValues();
        _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out token);
        var truc = token.ToString().Split(" ");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(truc[1]);
        var isAdmin = securityToken.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
        
        if (isAdmin is null)
            throw new BadHttpRequestException("UserId missing");

        return isAdmin == "True";
    }
    
    protected bool IsBotaniste()
    {
        StringValues token = new StringValues();
        _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out token);
        var truc = token.ToString().Split(" ");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(truc[1]);
        var isBotaniste = securityToken.Claims.FirstOrDefault(c => c.Type == "IsBotaniste")?.Value;
        
        if (isBotaniste is null)
            throw new BadHttpRequestException("UserId missing");

        return isBotaniste == "True";
    }
}