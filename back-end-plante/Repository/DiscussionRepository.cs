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
            discussion.Id.UserId1 == userId || discussion.Id.UserId2 == userId
        );
        
        var options = new FindOptions<Discussion>
        {
            Projection = Builders<Discussion>.Projection.Exclude(discussion => discussion.Messages)
        };

        var response = await _messagingCollection.FindAsync(filter, options);

        return response.ToList();
    }

    public async Task<Discussion> GetMessagesByDiscution(DiscussionId discussionId, string userId)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion =>
                discussion.Id.Equals(discussionId)    
                && (discussion.Id.UserId1 == userId || discussion.Id.UserId2 == userId)
        );

        var response = await _messagingCollection.FindAsync(filter);
        return response.FirstOrDefault();
    }

    public async Task CreateDiscution(Discussion discussion)
    {
        await _messagingCollection.InsertOneAsync(discussion);
    }

    public async Task SendMessage(SendMessageRequest request, string userId)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion =>
            discussion.Id.Equals(request.DiscussionId)    
            && discussion.Id.UserId1 == userId || discussion.Id.UserId2 == userId
        );
        
        var updates = Builders<Discussion>.Update.Push(discussion => discussion.Messages, request.Message);

        await _messagingCollection.UpdateOneAsync(filter, updates);
    }

    public async Task<long> DeleteDiscussionById(DiscussionId discussionId, string userId)
    {
        var filter = Builders<Discussion>.Filter.Where(discussion => 
            discussion.Id.Equals(discussionId) && (discussion.Id.UserId1 == userId || discussion.Id.UserId2 == userId)
        );

        var response = await _messagingCollection.DeleteOneAsync(filter);

        return response.DeletedCount;
    }
}