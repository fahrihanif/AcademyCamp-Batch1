namespace API.DTOs.Requests;

public record UserRequestDto(Guid EmployeeId, string UserName, string Password);
