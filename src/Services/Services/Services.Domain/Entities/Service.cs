using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Price { get; set; } 

    public Status Status { get; set; }

    public int ServiceCategoryId { get; set; }

    public ServiceCategory ServiceCategory { get; set; }

    public int SpecializationId { get; set; }

    public Specialization Specialization { get; set; }
}
