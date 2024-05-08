using Profiles.Contracts.DTOs.PersonalInfo;

namespace Profiles.Contracts.DTOs.Receptionist;

public class ReceptionistResponseDTO
{
    public Guid ReceptionistId { get; set; }

    public PersonalInfoResponseDTO PersonalInfo { get; set; }
}
