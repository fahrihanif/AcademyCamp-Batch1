using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync(LoginRequestDto requestDto)
    {
        await _userService.LoginUserAsync(requestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Login user is success."));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterRequestDto requestDto)
    {
        await _userService.RegisterUserAsync(requestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Register user is success."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserAsync()
    {
        var result = await _userService.GetAllAsync();

        if (!(result ?? Array.Empty<UserResponseDto>()).Any()) throw new NullReferenceException("User data is empty.");

        return Ok(new DataResponseDto<UserResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        var result = await _userService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("User Id not found.");

        return Ok(new SingleResponseDto<UserResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(UserRequestDto regionRequestDto)
    {
        await _userService.CreateAsync(regionRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(Guid id, UserRequestDto regionRequestDto)
    {
        var result = await _userService.UpdateAsync(id, regionRequestDto);

        if (!result) throw new NullReferenceException("User Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var isDeleted = await _userService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("User Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully deleted."));
    }
}
