namespace API.DTOs.Responses;

public record EmployeeResponseDto(
    Guid Id,
    string Nik,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateOnly HireDate,
    int Salary,
    float? ComissionPct,
    Guid? ManagerId,
    Guid JobId,
    Guid DepartmentId);
