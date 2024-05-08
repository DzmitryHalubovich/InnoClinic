using Profiles.Contracts.DTOs.PersonalInfo;

namespace Profiles.Contracts.DTOs.Receptionist;

public class ReceptionistUpdateDTO
{
    public PersonalInfoUpdateDTO PersonalInfo { get; set; }
    public string OfficeId { get; set; }
}
