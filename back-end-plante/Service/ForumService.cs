using back_end_plante.Common.Models;
using back_end_plante.Common.Requests;
using back_end_plante.Repository.Interfaces;
using back_end_plante.Service.Interfaces;

namespace back_end_plante.Service;

public class ForumService : IForumService
{
    private readonly IForumRepository _forumRepository;
    
    public ForumService(IForumRepository forumRepository)
    {
        _forumRepository = forumRepository;
    }

    public Task<List<Forum>> GetForums()
    {
        return _forumRepository.GetForums();
    }
    
    public Task<Forum> GetForumById(string forumId)
    {
        return _forumRepository.GetForumById(forumId);
    }
    
    public Task CreateQuestion(CreateQuestionRequest request)
    {
        return _forumRepository.CreateForum(request.ToForum());
    }

    public async Task AddResponse(AddReponseRequest request)
    {
        var forum = await _forumRepository.GetForumById(request.ForumId);

        forum.BotanistId = request.BotanistId;
        forum.Response = request.Response;

        await _forumRepository.UpdateForum(request.ForumId, request.ToForum());
    }

    public Task UpdateForum(Forum request)
    {
        return _forumRepository.UpdateForum(request.Id, request);
    }

    public Task DeleteForum(string forumId)
    {
        return _forumRepository.DeleteForum(forumId);
    }
}