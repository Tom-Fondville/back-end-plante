using back_end_plante.Common.Models;

namespace back_end_plante.Repository.Interfaces;

public interface IForumRepository
{
    Task<List<Forum>> GetForums();
    Task<Forum> GetForumById(string forumId);
    Task CreateForum(Forum forum);
    Task DeleteForum(string forumId);
    Task UpdateForum(string forumId, Forum forum);
}