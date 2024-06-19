using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class DepartmentController : BaseController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartmentAsync()
    {
        var result = await _departmentService.GetAllAsync();

        if (!(result ?? Array.Empty<DepartmentResponseDto>()).Any())
            throw new NullReferenceException("Department data is empty.");

        return Ok(new DataResponseDto<DepartmentResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentByIdAsync(Guid id)
    {
        var result = await _departmentService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("Department Id not found.");

        return Ok(new SingleResponseDto<DepartmentResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartmentAsync(DepartmentRequestDto regionRequestDto)
    {
        await _departmentService.CreateAsync(regionRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartmentAsync(Guid id, DepartmentRequestDto regionRequestDto)
    {
        var result = await _departmentService.UpdateAsync(id, regionRequestDto);

        if (!result) throw new NullReferenceException("Department Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDepartmentAsync(Guid id)
    {
        var isDeleted = await _departmentService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("Department Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully deleted."));
    }
}
