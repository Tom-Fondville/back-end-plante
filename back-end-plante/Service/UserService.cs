using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using back_end_plante.Common.Extensions;
using back_end_plante.Common.Models;
using back_end_plante.Common.Requests.user;
using back_end_plante.Common.Responses.User;
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

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetUserByMailAndPassword(loginRequest.Email, Hasher.Hash(loginRequest.Password));
        var token = GenerateToken(user);
        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationDate = token.ValidTo,
            UserId = user.Id
        };
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

    public Task<List<User>> GetUsers()
    {
        return _userRepository.GetUsers();
    }

    public Task UpdateUser(User userRequest)
    {
        return _userRepository.UpdateUser(userRequest);
    }

    public Task DeleteUserById(string id)
    {
        return _userRepository.DeleteUserById(id);
    }

    public Task AddAdresse(string userId, List<Adress> adresses)
    {
        return _userRepository.AddAdresse(userId, adresses);
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