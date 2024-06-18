namespace API.DTOs.Responses;

public record ErrorResponseDto(int Code, string Message, Dictionary<string, string[]?> ErrorDetails);
