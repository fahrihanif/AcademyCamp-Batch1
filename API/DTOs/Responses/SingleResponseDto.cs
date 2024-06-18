namespace API.DTOs.Responses;

public record SingleResponseDto<TEntity>(int Code, string Message, TEntity Data);
