namespace API.DTOs.Requests;

public record DepartmentRequestDto(string Name, Guid? ManagerId, Guid LocationId);
