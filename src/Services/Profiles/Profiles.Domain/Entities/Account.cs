using System.ComponentModel.DataAnnotations;

namespace Profiles.Domain.Entities;

public class Account
{
    public Guid AccountId { get; set; }

    [Required]
    public Guid PersonalInfoId { get; set; }
    public PersonalInfo PersonalInfo { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string? Email { get; set; }

    public bool IsEmailVerified { get; set; } = false;
    public Guid? PhotoId { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
