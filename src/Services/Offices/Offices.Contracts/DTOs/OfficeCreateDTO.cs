namespace Offices.Contracts.DTOs;

public record OfficeCreateDTO(string Address,
    string PhotoId, string RegistryPhoneNumber, bool IsActive);
