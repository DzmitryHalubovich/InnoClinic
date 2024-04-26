namespace Offices.Contracts.DTOs;

public record OfficeResponseDTO(string Id, string Address, 
    string PhotoId, string RegistryPhoneNumber, bool IsActive);
