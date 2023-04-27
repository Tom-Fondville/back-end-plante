using back_end_plante.Common.Models;

namespace back_end_plante.Repository.Interfaces;

public interface IUserRepository
{
    Task Register(User user);
    Task<User> GetUserById(string id);
    Task<User> GetUserByMailAndPassword(string mail, string password);
    Task UpdateUser(string userId, User user);
    Task DeleteUserById(string id);
}