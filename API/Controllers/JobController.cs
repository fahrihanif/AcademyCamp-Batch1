using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class JobController : BaseController
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobAsync()
    {
        var result = await _jobService.GetAllAsync();

        if (!(result ?? Array.Empty<JobResponseDto>()).Any()) throw new NullReferenceException("Job data is empty.");

        return Ok(new DataResponseDto<JobResponseDto>(StatusCodes.Status200OK, "Data found.", result!));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobByIdAsync(Guid id)
    {
        var result = await _jobService.GetByIdAsync(id);

        if (result is null) throw new NullReferenceException("Job Id not found.");

        return Ok(new SingleResponseDto<JobResponseDto>(StatusCodes.Status200OK, "Data found.", result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateJobAsync(JobRequestDto regionRequestDto)
    {
        await _jobService.CreateAsync(regionRequestDto);

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully created."));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobAsync(Guid id, JobRequestDto regionRequestDto)
    {
        var result = await _jobService.UpdateAsync(id, regionRequestDto);

        if (!result) throw new NullReferenceException("Job Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully updated."));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteJobAsync(Guid id)
    {
        var isDeleted = await _jobService.DeleteAsync(id);

        if (!isDeleted) throw new NullReferenceException("Job Id not found.");

        return Ok(new MessageResponseDto(StatusCodes.Status200OK, "Data successfully deleted."));
    }
}
