namespace API.DTOs.Requests;

public record RegisterRequestDto(
    string FirstName,
    string? LastName,
    string Email,
    string PhoneNumber,
    DateOnly HireDate,
    int Salary,
    decimal? ComissionPct,
    Guid? ManagerId,
    Guid JobId,
    Guid DepartmentId,
    string UserName,
    string Password,
    string ConfirmPassword
);
