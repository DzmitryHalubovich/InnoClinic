namespace Offices.Contracts.DTOs;

public record OfficeCreateDTO(string Address,
    string Photo_Id, string Registry_phone_number, bool IsActive);
