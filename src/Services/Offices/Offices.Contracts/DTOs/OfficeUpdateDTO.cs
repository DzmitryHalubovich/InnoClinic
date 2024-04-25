namespace Offices.Contracts.DTOs;

public record OfficeUpdateDTO(string Address, string Photo_Id, 
    string Registry_phone_number, bool IsActive);
