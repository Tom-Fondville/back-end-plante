using back_end_plante.Common.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Requests;

public class PlantRequest
{

    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("userId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId { get; set; }
    
    [BsonElement("advice")]
    public string Advice { get; set; }
    
    [BsonElement("pictures")]
    public Picture pictures { get; set; }



    public Plant toPlant()
    {
        Plant plant = new Plant();

        plant.Name = this.Name;
        plant.UserId = this.UserId;
        plant.Advice = this.Advice;
        plant.pictures = this.pictures;

        return plant;

    }


}