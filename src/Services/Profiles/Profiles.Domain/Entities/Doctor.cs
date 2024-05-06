using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Profiles.Domain.Entities.OuterServicesModels;

namespace Profiles.Domain.Entities;

public class Doctor
{
    public Guid DoctorId { get; set; }
    [Required]
    public DateTime CareerStartYear { get; set; }
    [Required]
    [MaxLength(60)]
    public string Status { get; set; } = null!;

    
    [Required]
    [MaxLength(24)]
    public string OfficeId { get; set; }
    public Office Office { get; set; }

    [Required]
    public Guid AccountId { get; set; }
    public Account Account { get; set; }

    [Required]
    public Guid SpecializationId { get; set; }
    public Specialization Specialization { get; set; }
}
