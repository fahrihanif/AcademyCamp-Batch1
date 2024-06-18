using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RegionController : BaseController
{
    private readonly IRegionService _regionService;

    public RegionController(IRegionService regionService)
    {
        _regionService = regionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRegionAsync()
    {
        var result = await _regionService.GetAllAsync();

        if (!(result ?? Array.Empty<RegionResponseDto>()).Any())
            throw new NullReferenceException("Region data is empty.");

        return Ok(new DataResponseDto<RegionResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRegionByIdAsync(Guid id)
    {
        var result = await _regionService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("Region id not found.");

        return Ok(new SingleResponseDto<RegionResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegionAsync(RegionRequestDto regionRequestDto)
    {
        await _regionService.CreateAsync(regionRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegionAsync(Guid id, RegionRequestDto regionRequestDto)
    {
        var isUpdated = await _regionService.UpdateAsync(id, regionRequestDto);

        if (!isUpdated) throw new NullReferenceException("Region id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRegionAsync(Guid id)
    {
        var isDeleted = await _regionService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("Region id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully deleted."));
    }
}
