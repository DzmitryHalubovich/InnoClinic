using System.ComponentModel.DataAnnotations;

namespace Profiles.Domain.Entities.OuterServicesModels;

public class Office
{
    [Key]
    public string OfficeId { get; set; }
    public string Address { get; set; } = null!;
    public string? PhotoId { get; set; }
    public string RegistryPhoneNumber { get; set; } = null!;
    public bool IsActive { get; set; }
}
