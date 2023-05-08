using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using back_end_plante.Common.Extensions;
using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Common.Requests.user;
using back_end_plante.Repository.Interfaces;
using back_end_plante.Service.Interfaces;
using back_end_plante.Utils;
using Microsoft.IdentityModel.Tokens;

namespace back_end_plante.Service;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<JwtSecurityToken> Login(string email, string password)
    {
        var user = await _userRepository.GetUserByMailAndPassword(email, Hasher.Hash(password));
        return GenerateToken(user);

    }
    
    public Task Register(UserRequest userRequest)
    {
        if (!userRequest.IsValid()) throw new BadHttpRequestException("UserRequest not valid");
        return _userRepository.Register(userRequest.ToUser());
    }

    public Task<User> GetUserById(string id)
    {
        return _userRepository.GetUserById(id);
    }

    public Task UpdateUser(string userId, UserRequest userRequest)
    {
        if (!userRequest.IsValid()) throw new BadHttpRequestException("UserRequest not valid");
        return _userRepository.UpdateUser(userId ,userRequest.ToUser());
    }

    public Task DeleteUserById(string id)
    {
        return _userRepository.DeleteUserById(id);
    }
    
    private JwtSecurityToken GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.Mail),
            new Claim("userName",user.Name),
            new Claim("userId",user.Id),
        };
        return new JwtSecurityToken
        (
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials
        );
    }
}