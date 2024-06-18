namespace API.Models;

public class History
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid JobId { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
    public Department? Department { get; set; }
    public Job? Job { get; set; }
}
