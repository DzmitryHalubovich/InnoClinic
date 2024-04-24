namespace Offices.Contracts.DTOs;

public class OfficeResponseDTO
{
    public string Id { get; set; }
    public string Address { get; set; }
    public string Photo_Id { get; set; }
    public string Registry_phone_number { get; set; }
    public bool IsActive { get; set; }
}
