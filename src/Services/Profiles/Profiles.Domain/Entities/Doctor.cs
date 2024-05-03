using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Profiles.Domain.Entities;

public class Doctor
{
    public Guid DoctorId { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public DateTime CareerStartYear { get; set; }
    public string OfficeId { get; set; }
    [Required]
    public string Status { get; set; } = null!;

    [Required]
    [ForeignKey("Specialization")]
    public Guid? SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }


    [NotMapped]
    public Office? Office { get; set; }
}
