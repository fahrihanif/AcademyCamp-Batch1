using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class LocationController : BaseController
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLocationAsync()
    {
        var result = await _locationService.GetAllAsync();

        if (!(result ?? Array.Empty<LocationResponseDto>()).Any())
            throw new NullReferenceException("Location data is empty.");

        return Ok(new DataResponseDto<LocationResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocationByIdAsync(Guid id)
    {
        var result = await _locationService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("Location Id not found.");

        return Ok(new SingleResponseDto<LocationResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocationAsync(LocationRequestDto regionRequestDto)
    {
        await _locationService.CreateAsync(regionRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocationAsync(Guid id, LocationRequestDto regionRequestDto)
    {
        var result = await _locationService.UpdateAsync(id, regionRequestDto);

        if (!result) throw new NullReferenceException("Location Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLocationAsync(Guid id)
    {
        var isDeleted = await _locationService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("Location Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully deleted."));
    }
}
