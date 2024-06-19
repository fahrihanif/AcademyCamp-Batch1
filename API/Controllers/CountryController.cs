using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class CountryController : BaseController
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCountryAsync()
    {
        var result = await _countryService.GetAllAsync();

        if (!(result ?? Array.Empty<CountryResponseDto>()).Any())
            throw new NullReferenceException("Country data is empty.");

        return Ok(new DataResponseDto<CountryResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryByIdAsync(Guid id)
    {
        var result = await _countryService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("Country id not found.");

        return Ok(new SingleResponseDto<CountryResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountryAsync(CountryRequestDto countryRequestDto)
    {
        await _countryService.CreateAsync(countryRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountryAsync(Guid id, CountryRequestDto countryRequestDto)
    {
        var result = await _countryService.UpdateAsync(id, countryRequestDto);

        if (!result) throw new NullReferenceException("Country id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRegionAsync(Guid id)
    {
        var isDeleted = await _countryService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("Country id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }
}
