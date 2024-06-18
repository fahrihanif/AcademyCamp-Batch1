using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Employee : BaseEntity
{
    public string GetFullName()
    {
        return string.Concat(FirstName, " ", LastName);
    }
    [MaxLength(6)] public string Nik { get; set; } = string.Empty;

    [MaxLength(50)] public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)] public string? LastName { get; set; }

    [MaxLength(50)] public string Email { get; set; } = string.Empty;

    [MaxLength(20)] public string PhoneNumber { get; set; } = string.Empty;

    public DateOnly HireDate { get; set; }
    public int Salary { get; set; }
    public float? ComissionPct { get; set; }
    public Guid? ManagerId { get; set; }
    public Guid JobId { get; set; }
    public Guid DepartmentId { get; set; }

    // Cardinality
    public ICollection<Employee>? Employees { get; set; }
    public Employee? Manager { get; set; }
    public Department? Department { get; set; }
    public Department? DepartmentManager { get; set; }
    public ICollection<History>? Histories { get; set; }
    public Job? Job { get; set; }
    public User? User { get; set; }
}
