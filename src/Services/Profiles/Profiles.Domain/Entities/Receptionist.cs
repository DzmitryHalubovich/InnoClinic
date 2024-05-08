using System.ComponentModel.DataAnnotations;

namespace Profiles.Domain.Entities;

public class Receptionist
{
    [Required]
    public Guid ReceptionistId { get; set; }

    [Required]
    [MaxLength(24)]
    public string OfficeId { get; set; }


    [Required]
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
}
