using back_end_plante.Common.Enums;
using back_end_plante.Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Requests;

public class AnnonceRequest
{
    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }
    
    [BsonElement("endDate")]
    public DateTime EndDate { get; set; }
    
    [BsonElement("adress")]
    public Adress Adress { get; set; }

    [BsonElement("plantsId")]
    public List<string> PlantsId { get; set; }
    
    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }
    
    [BsonElement("status")]
    public AnnonceStatus Status { get; set; }

    public Annonce ToAnnonce()
    {
        return new Annonce
        {
            StartDate = StartDate,
            EndDate = EndDate,
            Adress = Adress,
            PossiblesGardiensId = new List<string>(),
            PlantsId = PlantsId,
            UserId = UserId,
            Status = AnnonceStatus.Waiting
        };
    }
}