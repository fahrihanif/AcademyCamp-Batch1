namespace API.DTOs.Requests;

public record JobRequestDto(string Title, int MinSalary, int MaxSalary);
