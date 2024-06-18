using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.Interfaces;

public interface IJobService : IGeneralService<JobRequestDto, JobResponseDto> { }
