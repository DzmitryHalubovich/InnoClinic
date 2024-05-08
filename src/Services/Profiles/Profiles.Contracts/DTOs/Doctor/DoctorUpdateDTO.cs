using Profiles.Contracts.DTOs.PersonalInfo;

namespace Profiles.Contracts.DTOs.Doctor;

public class DoctorUpdateDTO
{
    public PersonalInfoUpdateDTO? PersonalInfo { get; set; }
    public Guid SpecializationId { get; set; }
    public string OfficeId { get; set; }
    public DateTime CareerStartYear { get; set; }
    public string Status { get; set; }
}
