using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class Picture
{
    [BsonElement("url")]
    public string Url { get; set; }
    
    [BsonElement("isEphemere")]
    public bool IsEphemere { get; set; }
}