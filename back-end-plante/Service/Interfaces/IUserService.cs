using System.IdentityModel.Tokens.Jwt;
using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Common.Requests.user;

namespace back_end_plante.Service.Interfaces;

public interface IUserService
{
    Task<JwtSecurityToken> Login(string email, string password);
    Task Register(UserRequest user);
    Task<User> GetUserById(string id);
    Task UpdateUser(string userId ,UserRequest userRequest);
    Task DeleteUserById(string id);
}