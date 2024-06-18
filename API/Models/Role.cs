using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Role : BaseEntity
{
    [MaxLength(50)] public string Name { get; set; } = string.Empty;

    // Cardinality
    public ICollection<UserRole>? UserRoles { get; set; }
}
