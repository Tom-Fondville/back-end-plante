using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;

namespace back_end_plante.Service.Interfaces;

public interface IForumService
{
    Task<List<Forum>> GetForums();
    Task<Forum> GetForumById(string forumId);
    Task CreateQuestion(CreateQuestionRequest request);
    Task AddResponse(AddReponseRequest request);
    Task UpdateForum(Forum request);
    Task DeleteForum(string forumId);
}