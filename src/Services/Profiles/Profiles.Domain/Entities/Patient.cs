using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Profiles.Domain.Entities;

public class Patient
{
    public Guid PatientId { get; set; }

    [Required]
    [ForeignKey("Account")]
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }

    public Guid? PhotoId { get; set; }
}
