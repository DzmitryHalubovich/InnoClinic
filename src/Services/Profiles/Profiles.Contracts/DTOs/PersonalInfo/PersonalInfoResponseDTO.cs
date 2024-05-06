namespace Profiles.Contracts.DTOs.PersonalInfo;

public class PersonalInfoResponseDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
}
