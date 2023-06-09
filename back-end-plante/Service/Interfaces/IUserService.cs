using back_end_plante.Common.Models;
using back_end_plante.Common.Requests.user;
using back_end_plante.Common.Responses.User;

namespace back_end_plante.Service.Interfaces;

public interface IUserService
{
    Task<LoginResponse> UserLogin(LoginRequest loginRequest);
    Task<LoginResponse> AdminLogin(LoginRequest loginRequest);
    Task Register(UserRequest user);
    Task<User> GetUserById(string id);
    Task<List<User>> GetUsers();
    Task UpdateUser(User userRequest);
    Task DeleteUserById(string id);
    Task AddAdresse(string userId, List<Adress> adresses);
}