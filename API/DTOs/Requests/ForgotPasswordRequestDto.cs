namespace API.DTOs.Requests;

public record ForgotPasswordRequestDto(
    string EmailOrUsername,
    int Otp,
    string Password,
    string ConfirmPassword);
