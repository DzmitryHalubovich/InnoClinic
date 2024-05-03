namespace Profiles.Contracts.DTOs;

public class DoctorResponseDTO()
{
    public Guid DoctorId { get; set; }
    public string FullName { get; set; }
    public int Experience { get; set;}
    public string OfficeAddress { get; set; }
    public string Status { get; set;}
    public string Specialization {  get; set; }
}

