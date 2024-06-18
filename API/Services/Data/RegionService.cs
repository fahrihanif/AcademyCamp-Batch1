using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Data;

public class RegionService : GeneralService<IRegionRepository, RegionRequestDto, RegionResponseDto, Region>,
                             IRegionService
{
    public RegionService(IRegionRepository repository, IMapper mapper, ITransactionRepository transactionRepository) :
        base(repository, mapper, transactionRepository) { }
}
