using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Job : BaseEntity
{
    [MaxLength(50)] public string Title { get; set; } = string.Empty;

    public int MinSalary { get; set; }
    public int MaxSalary { get; set; }

    // Cardinality
    public ICollection<History>? Histories { get; set; }
    public ICollection<Employee>? Employees { get; set; }
}
