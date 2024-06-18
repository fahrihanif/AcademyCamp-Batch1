namespace API.DTOs.Responses;

public record UserResponseDto(
    Guid EmployeeId,
    string UserName,
    string Password,
    int Otp,
    DateTime ExpiredOtp,
    bool IsOtpUsed);
