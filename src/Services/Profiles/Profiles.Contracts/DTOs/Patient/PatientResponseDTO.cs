using Profiles.Contracts.DTOs.PersonalInfo;

namespace Profiles.Contracts.DTOs.Patient;

public class PatientResponseDTO
{
    public Guid PatientId { get; set; }

    public PersonalInfoResponseDTO? PersonalInfo { get; set;}
}
