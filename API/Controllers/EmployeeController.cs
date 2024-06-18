using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EmployeeController : BaseController
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("Details")]
    public async Task<IActionResult> GetAllEmployeeDetailAsync([FromQuery]EmployeeDetailRequestDto requestDto)
    {
        var result = await _employeeService.GetEmployeeDetails(requestDto);
        return Ok(new DataPaginationResponseDto<EmployeeDetailResponseDto>(StatusCodes.Status200OK, "Data found.", requestDto.PageIndex,requestDto.PageSize, result.count , result.mapEmployeeDetail));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployeeAsync()
    {
        var result = await _employeeService.GetAllAsync();

        if (!(result ?? Array.Empty<EmployeeResponseDto>()).Any())
            throw new NullReferenceException("Employee data is empty.");

        return Ok(new DataResponseDto<EmployeeResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
    {
        var result = await _employeeService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("Employee Id not found.");

        return Ok(new SingleResponseDto<EmployeeResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync(EmployeeRequestDto regionRequestDto)
    {
        await _employeeService.CreateAsync(regionRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeAsync(Guid id, EmployeeRequestDto regionRequestDto)
    {
        var result = await _employeeService.UpdateAsync(id, regionRequestDto);

        if (!result) throw new NullReferenceException("Employee Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
    {
        var isDeleted = await _employeeService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("Employee Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully deleted."));
    }
}
