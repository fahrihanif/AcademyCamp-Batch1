namespace API.DTOs.Responses;

public record DataResponseDto<TResponse>(int Code, string Message, IEnumerable<TResponse> Data);
