namespace API.DTOs.Responses;

public record JobResponseDto(Guid Id, string Title, int MinSalary, int MaxSalary);
