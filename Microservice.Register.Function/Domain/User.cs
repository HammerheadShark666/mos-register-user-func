using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microservice.Register.Function.Helpers.Enums;

namespace Microservice.Register.Function.Domain;

[Table("MSOS_User")]
public class User
{
    public User() { }

    public User(Guid id, string email, string hashedPassword, Role role)
    {
        Id = id;
        Email = email;
        PasswordHash = hashedPassword;
        Role = role;
    }

    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    [Required]
    public Role Role { get; set; }
    public string VerificationToken { get; set; } = string.Empty;
    public DateTime? Verified { get; set; }
    public bool IsAuthenticated => Verified.HasValue;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    [NotMapped]
    public string Password { get; set; } = string.Empty;

    [NotMapped]
    public string ConfirmPassword { get; set; } = string.Empty;
}