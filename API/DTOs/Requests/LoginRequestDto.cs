namespace API.DTOs.Requests;

public record LoginRequestDto(string EmailOrUsername, string Password);
