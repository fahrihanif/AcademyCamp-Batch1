using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.Interfaces;

public interface IRegionService : IGeneralService<RegionRequestDto, RegionResponseDto> { }
