using Profiles.Contracts.DTOs.OuterServicesModels;

namespace Profiles.Contracts.DTOs.Doctor;

public class DoctorResponseDTO()
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int Experience { get; set; }

    public StatusDTO Status { get; set; } 

    public OfficeDTO? Office { get; set; }

    public  Guid SpecializationId { get; set; }
}

