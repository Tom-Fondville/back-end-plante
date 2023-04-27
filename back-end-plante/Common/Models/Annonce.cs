using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class Annonce
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }
    //DateFin
    //Lieux object 
    //Liste d'id de gardiens possibles
    //List d'id plantes à garder
    //Id du proprio des plantes
    //Status de l'annonce (enum)
}