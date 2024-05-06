using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Profiles.Domain.Entities.OuterServicesModels;

[NotMapped]
public class Office
{
    public string OfficeId { get; set; }
    public string Address { get; set; } = null!;
    public string? PhotoId { get; set; }
    public string RegistryPhoneNumber { get; set; } = null!;
    public bool IsActive { get; set; }
}
