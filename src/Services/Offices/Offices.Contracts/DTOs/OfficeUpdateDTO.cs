namespace Offices.Contracts.DTOs;

public record OfficeUpdateDTO(string Address, string PhotoId, 
    string RegistryPhoneNumber, bool IsActive);
