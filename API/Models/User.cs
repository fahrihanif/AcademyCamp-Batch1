using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class User
{
    [Key] public Guid EmployeeId { get; set; }

    [MaxLength(50)] public string UserName { get; set; } = string.Empty;

    [MaxLength(255)] public string Password { get; set; } = string.Empty;

    public int Otp { get; set; }
    public DateTime ExpiredOtp { get; set; }
    public bool IsOtpUsed { get; set; }

    // Cardinality
    public ICollection<UserRole>? UserRoles { get; set; }
    public Employee? Employee { get; set; }
}
