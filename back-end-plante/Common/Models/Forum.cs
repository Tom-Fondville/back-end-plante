using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class Forum
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("requestorId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string RequestorId { get; set; }
    
    [BsonElement("BotanistId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string BotanistId { get; set; }
    
    [BsonElement("questionType")]
    public string QuestionType { get; set; }
    
    [BsonElement("question")]
    public string Question { get; set; }
    
    [BsonElement("response")]
    public string Response { get; set; }
}