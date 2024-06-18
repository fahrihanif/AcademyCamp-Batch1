using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Data;

public class CountryService : GeneralService<ICountryRepository, CountryRequestDto, CountryResponseDto, Country>,
                              ICountryService
{
    private readonly IRegionRepository _regionRepository;

    public CountryService(ICountryRepository repository, IMapper mapper, ITransactionRepository transactionRepository,
                          IRegionRepository regionRepository) :
        base(repository, mapper, transactionRepository)
    {
        _regionRepository = regionRepository;
    }

    public override async Task CreateAsync(CountryRequestDto request)
    {
        await CheckNullReference(request.RegionId, _regionRepository, nameof(request.RegionId));
        await base.CreateAsync(request);
    }

    public override async Task<bool> UpdateAsync(Guid id, CountryRequestDto request)
    {
        await CheckNullReference(request.RegionId, _regionRepository, nameof(request.RegionId));
        return await base.UpdateAsync(id, request);
    }
}
