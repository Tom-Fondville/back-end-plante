using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("surName")]
    public string SurName { get; set; }
    
    [BsonElement("mail")]
    public string Mail { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("phoneNumber")]
    public long PhoneNumer { get; set; }
    
    [BsonElement("isAdmin")]
    public bool IsAdmin { get; set; }
    
    [BsonElement("isBotaniste")]
    public bool IsBotaniste { get; set; }
    
    [BsonElement("adresses")]
    public List<Adress> Adresses { get; set; }
    
    //avatar
}