using System.ComponentModel.DataAnnotations;

namespace Profiles.Domain.Entities;

public class Specialization
{
    public Guid SpecializationId { get; set; }
    [Required]
    [MaxLength(200)]
    public string SpecializationName { get; set; } = null!;
    [Required]
    public bool IsActive { get; set; } 
}
