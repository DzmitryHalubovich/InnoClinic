using System.ComponentModel.DataAnnotations;

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
    [Required]
    public string Status { get; set; } = null!;
}
