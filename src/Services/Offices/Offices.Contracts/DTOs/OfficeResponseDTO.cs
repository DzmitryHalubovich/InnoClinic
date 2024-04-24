namespace Offices.Contracts.DTOs;

public record OfficeResponseDTO(string Id, string Address, 
    string Photo_Id, string Registry_phone_number, bool IsActive);
