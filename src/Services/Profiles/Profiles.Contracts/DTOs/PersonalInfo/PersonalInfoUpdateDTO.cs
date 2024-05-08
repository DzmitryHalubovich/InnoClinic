namespace Profiles.Contracts.DTOs.PersonalInfo;

public class PersonalInfoUpdateDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
}
