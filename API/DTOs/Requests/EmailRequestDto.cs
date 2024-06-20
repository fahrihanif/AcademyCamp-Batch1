namespace API.DTOs.Requests;

public record EmailRequestDto(string To, string FullName, string Subject, string Body);
