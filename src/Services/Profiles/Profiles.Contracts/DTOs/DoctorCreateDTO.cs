using System.ComponentModel.DataAnnotations;

namespace Profiles.Contracts.DTOs;

public class DoctorCreateDTO
{
    public AccountPersonalInfoCreateDTO PersonalInfo { get; set; }
    public Guid SpecializationId { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string OfficeId { get; set; }
    public string Status { get; set; }
}
