using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class RoleController : BaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllRoleAsync()
    {
        var result = await _roleService.GetAllAsync();

        if (!(result ?? Array.Empty<RoleResponseDto>()).Any()) throw new NullReferenceException("Role data is empty.");

        return Ok(new DataResponseDto<RoleResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleByIdAsync(Guid id)
    {
        var result = await _roleService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("Role Id not found.");

        return Ok(new SingleResponseDto<RoleResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(RoleRequestDto regionRequestDto)
    {
        await _roleService.CreateAsync(regionRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoleAsync(Guid id, RoleRequestDto regionRequestDto)
    {
        var result = await _roleService.UpdateAsync(id, regionRequestDto);

        if (!result) throw new NullReferenceException("Role Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        var isDeleted = await _roleService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("Role Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully deleted."));
    }
}
