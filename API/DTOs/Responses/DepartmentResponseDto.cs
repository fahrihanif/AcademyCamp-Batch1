namespace API.DTOs.Responses;

public record DepartmentResponseDto(Guid Id, string Name, Guid? ManagerId, Guid LocationId);
