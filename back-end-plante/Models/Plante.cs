using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Models;

public class Plante
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("nom")]
    public string nom { get; set; }
}