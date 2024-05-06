using Profiles.Contracts.DTOs.PersonalInfo;

namespace Profiles.Contracts.DTOs.Patient;

public class PatientCreateDTO
{
    public PersonalInfoCreateDTO PersonalInfo { get; set; }

    public string Email { get; set; }
}
