namespace API.DTOs.Requests;

public record EmployeeRequestDto(
    string FirstName,
    string? LastName,
    string Email,
    string PhoneNumber,
    DateOnly HireDate,
    int Salary,
    decimal? ComissionPct,
    Guid? ManagerId,
    Guid JobId,
    Guid DepartmentId);
