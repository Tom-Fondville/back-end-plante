using back_end_plante.Common.Models.Messaging;
using back_end_plante.Common.Requests;
using back_end_plante.Configurations;
using back_end_plante.Repository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace back_end_plante.Repository;

public class DiscussionRepository : MsprPlanteRepositoryBase, IDiscussionRepository
{
    private readonly IMongoCollection<Discussion> _messagingCollection;
    
    public DiscussionRepository(IOptions<ConnectionStringConfiguration> connectionStringConfiguration) : base(connectionStringConfiguration)
    {
        try
        {
            var db = GetClient().WithReadPreference(ReadPreference.Secondary).GetDatabase(MongoPocProviderName);
            _messagingCollection = db.GetCollection<Discussion>("Discussion");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Discussion>> GetDiscussions()
    {
        var filter = Builders<Discussion>.Filter.Empty;

        var response = await _messagingCollection.FindAsync(filter);

        return response.ToList();
    }
    
    
    public async Task<List<Discussion>> GetDiscussionsByUser(string userId)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion =>
            discussion.UserId1 == userId || discussion.UserId2 == userId
        );
        
        var options = new FindOptions<Discussion>
        {
            Projection = Builders<Discussion>.Projection.Exclude(discussion => discussion.Messages)
        };

        var response = await _messagingCollection.FindAsync(filter, options);

        return response.ToList();
    }

    public async Task<Discussion?> GetMessagesByDiscution(string discussionId, string userId)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion =>
                discussion.Id == discussionId    
                && (discussion.UserId1 == userId || discussion.UserId2 == userId)
        );

        var response = await _messagingCollection.FindAsync(filter);
        return response.FirstOrDefault();
    }

    public async Task<bool> CreateDiscution(Discussion discussion)
    {
        var discution = await GetDiscutionByusers(discussion.UserId1, discussion.UserId2);
        if (discution is null)
            await _messagingCollection.InsertOneAsync(discussion);
        
        return discution is null;
    }

    private async Task<Discussion?> GetDiscutionByusers(string userId1, string userId2)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion => 
            (discussion.UserId1 == userId1 && discussion.UserId2 == userId2)
            ||
            (discussion.UserId1 == userId2 || discussion.UserId2 == userId1)
        );
        var resposne = await _messagingCollection.FindAsync(filter);

        return resposne.FirstOrDefault();
    }

    public async Task SendMessage(SendMessageRequest request, string userId)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion =>
            discussion.Id == request.DiscussionId    
            && discussion.UserId1 == userId || discussion.UserId2 == userId
        );
        
        var updates = Builders<Discussion>.Update.Push(discussion => discussion.Messages, request.Message);

        await _messagingCollection.UpdateOneAsync(filter, updates);
    }

    public async Task<long> DeleteDiscussionById(string discussionId, string userId)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion => 
            discussion.Id == discussionId && (discussion.UserId1 == userId || discussion.UserId2 == userId)
        );

        var response = await _messagingCollection.DeleteOneAsync(filter);

        return response.DeletedCount;
    }
}