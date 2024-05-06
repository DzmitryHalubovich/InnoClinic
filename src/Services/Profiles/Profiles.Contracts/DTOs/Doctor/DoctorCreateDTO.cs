using System.ComponentModel.DataAnnotations;
using Profiles.Contracts.DTOs.PersonalInfo;

namespace Profiles.Contracts.DTOs.Doctor;

public class DoctorCreateDTO
{
    public PersonalInfoCreateDTO PersonalInfo { get; set; }
    public Guid SpecializationId { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string OfficeId { get; set; }
    public string Status { get; set; }
}
