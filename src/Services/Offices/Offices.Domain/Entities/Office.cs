using MongoDB.Bson.Serialization.Attributes;

namespace Offices.Domain.Entities;

public class Office
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("photo_id")]
    public string Photo_Id { get; set; }

    [BsonElement("registry_phone_number")]
    public string Registry_phone_number { get; set; }

    [BsonElement("isActive")]
    public bool IsActive { get; set; }
}
