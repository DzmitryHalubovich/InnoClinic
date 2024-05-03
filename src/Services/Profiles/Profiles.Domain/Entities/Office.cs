namespace Profiles.Domain.Entities;

public class Office
{
    public string Id { get; set; }

    public string Address { get; set; } = null!;
    public string? PhotoId { get; set; }
    public string RegistryPhoneNumber { get; set; } = null!;
    public bool IsActive { get; set; }
}
