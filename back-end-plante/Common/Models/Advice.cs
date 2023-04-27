using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class Advice
{
    [BsonElement("message")]
    public string Message { get; set; }
    
    [BsonElement("userId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId { get; set; }
}