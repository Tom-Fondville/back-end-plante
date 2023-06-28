using back_end_plante.Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class Annonce
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }
    
    [BsonElement("endDate")]
    public DateTime EndDate { get; set; }
    
    [BsonElement("adress")]
    public Adress Adress { get; set; }
    
    [BsonElement("possiblesGardiensId")]
    public List<string> PossiblesGardiensId { get; set; }
    
    [BsonElement("plantsId")]
    public List<string> PlantsId { get; set; }
    
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }
    
    [BsonElement("status")]
    public AnnonceStatus Status { get; set; }
}