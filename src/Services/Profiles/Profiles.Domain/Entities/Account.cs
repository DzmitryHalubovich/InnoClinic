using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Profiles.Domain.Entities;

public class Account
{
    public Guid AccountId { get; set; }

    [Required]
    public Guid PersonalInformationId { get; set; }
    public PersonalInformation? PersonalInfo { get; set; }


    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; }

    [NotMapped]
    public bool IsEmailVerified { get; set; }
    [NotMapped]
    public Guid PhotoId { get; set; }
    [NotMapped]
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    [NotMapped]
    public Guid UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }


}
