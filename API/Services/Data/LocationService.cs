using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Data;

public class LocationService : GeneralService<ILocationRepository, LocationRequestDto, LocationResponseDto, Location>,
                               ILocationService
{
    private readonly ICountryRepository _countryRepository;

    public LocationService(ILocationRepository repository, IMapper mapper, ITransactionRepository transactionRepository,
                           ICountryRepository countryRepository)
        : base(repository, mapper, transactionRepository)
    {
        _countryRepository = countryRepository;
    }

    public override async Task CreateAsync(LocationRequestDto request)
    {
        await CheckNullReference(request.CountryId, _countryRepository, nameof(request.CountryId));
        await base.CreateAsync(request);
    }

    public override async Task<bool> UpdateAsync(Guid id, LocationRequestDto request)
    {
        await CheckNullReference(request.CountryId, _countryRepository, nameof(request.CountryId));
        return await base.UpdateAsync(id, request);
    }
}
