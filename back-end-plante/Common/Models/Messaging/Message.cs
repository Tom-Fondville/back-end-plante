using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models.Messaging;

public class Message
{
    [BsonElement("userName")]
    public string UserName { get; set; }
    
    [BsonElement("message")]
    public string MessageText { get; set; }
    
    [BsonElement("date")]
    public DateTime Date { get; set; }
}