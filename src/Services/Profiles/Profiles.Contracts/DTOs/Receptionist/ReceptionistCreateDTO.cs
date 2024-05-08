using Profiles.Contracts.DTOs.PersonalInfo;

namespace Profiles.Contracts.DTOs.Receptionist;

public class ReceptionistCreateDTO
{
    public PersonalInfoCreateDTO PersonalInfo { get; set; }
    public string Email { get; set; }
    public string OfficeId { get; set; }
}
