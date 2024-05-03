using System.ComponentModel.DataAnnotations;

namespace Profiles.Domain.Entities;

public class Specialization
{
    public Guid SpecializationId { get; set; }
    [Required]
    public string SpecializationName { get; set; } = null!;
}
