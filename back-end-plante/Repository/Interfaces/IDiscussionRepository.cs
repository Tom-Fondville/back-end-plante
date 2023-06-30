using back_end_plante.Common.Models.Messaging;
using back_end_plante.Common.Requests;

namespace back_end_plante.Repository.Interfaces;

public interface IDiscussionRepository
{
    Task<List<Discussion>> GetDiscussions();
    Task<List<Discussion>> GetDiscussionsByUser(string userId);
    Task<Discussion> GetMessagesByDiscution(DiscussionId discussionId, string userId);
    Task SendMessage(SendMessageRequest request, string userId);
    Task<long> DeleteDiscussionById(DiscussionId discussionId, string userId);
}