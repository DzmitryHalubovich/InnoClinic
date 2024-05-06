using MongoDB.Bson.Serialization.Attributes;

namespace Offices.Domain.Entities;

public class Office
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string OfficeId { get; set; }

    [BsonElement("address")]
    public string Address { get; set; } = null!;

    [BsonElement("photoId")]
    public string? PhotoId { get; set; }

    [BsonElement("registryPhoneNumber")]
    public string RegistryPhoneNumber { get; set; } = null!;

    [BsonElement("isActive")]
    public bool IsActive { get; set; }
}
