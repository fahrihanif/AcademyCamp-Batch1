using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.Interfaces;

public interface IUserService : IGeneralService<UserRequestDto, UserResponseDto>
{
    Task RegisterUserAsync(RegisterRequestDto request);
    Task LoginUserAsync(LoginRequestDto request);
}
