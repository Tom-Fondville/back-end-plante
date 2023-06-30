using MongoDB.Bson.Serialization.Attributes;

namespace back_end_plante.Common.Models.Messaging;

public class DiscussionId
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId1 { get; set; }
    
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId2 { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (this == obj) return true;
        if (obj is not DiscussionId messagingId) return false;

        return UserId1 == messagingId.UserId1 && UserId2 == messagingId.UserId2;
    }
}