using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models;

public class Adress
{
    [BsonElement("adress")]
    public string AdressName { get; set; }
    
    [BsonElement("zipCode")]
    public int ZipCode { get; set; }
    
    [BsonElement("city")]
    public string City { get; set; }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(AdressName)
            || string.IsNullOrEmpty(City))
            return false;
        
        if (ZipCode is 0) return false;
        
        //TODO: Vérif si c'est bien un zipCode
        
        return true;
    }
}