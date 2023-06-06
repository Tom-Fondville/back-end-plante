using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class Plant
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("userId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId { get; set; }
    
    [BsonElement("advice")]
    public string Advice { get; set; }
    
    [BsonElement("pictures")]
    public Picture pictures { get; set; }
}