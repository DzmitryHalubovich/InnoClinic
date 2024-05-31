namespace Offices.Contracts.DTOs;

public record OfficeResponseDTO(string OfficeId, string Address, 
    string PhotoId, string RegistryPhoneNumber, bool IsActive);
