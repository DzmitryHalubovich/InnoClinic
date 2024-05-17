using Services.Contracts.Specialization;
using System.ComponentModel.DataAnnotations;

namespace Services.Contracts.Service;

public class ServiceResponseDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Status { get; set; }


    public ServiceCategoryDTO ServiceCategory { get; set; }

    public SpecializationResponseDTO Specialization { get; set; }
}
