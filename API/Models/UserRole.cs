namespace API.Models;

public class UserRole : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid RoleId { get; set; }

    // Cardinality
    public Role? Role { get; set; }
    public User? User { get; set; }
}
