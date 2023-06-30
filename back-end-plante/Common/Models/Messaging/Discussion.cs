using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models.Messaging;

public class Discussion
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("userId1")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId1 { get; set; }
    
    [BsonElement("userId2")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId2 { get; set; }
    
    [BsonElement("userName")]
    public string UserName { get; set; }

    [BsonElement("lastUpdate")]
    public DateTime LastUpdate { get; set; }

    [BsonElement("messages")]
    public List<Message> Messages { get; set; }
}   