using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models.Messaging;

public class Discussion
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.Document)]
    public DiscussionId Id { get; set; }
    
    [BsonElement("userName")]
    public string UserName { get; set; }

    [BsonElement("lastUpdate")]
    public DateTime LastUpdate { get; set; }

    [BsonElement("messages")]
    public List<Message> Messages { get; set; }
}