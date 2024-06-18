using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Department : BaseEntity
{
    [MaxLength(30)] public string Name { get; set; } = string.Empty;

    public Guid? ManagerId { get; set; }
    public Guid LocationId { get; set; }

    // Cardinality
    public Employee? Manager { get; set; }
    public ICollection<Employee>? Employees { get; set; }
    public Location? Location { get; set; }
    public ICollection<History>? Histories { get; set; }
}
