using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Data;

public class DepartmentService :
    GeneralService<IDepartmentRepository, DepartmentRequestDto, DepartmentResponseDto, Department>, IDepartmentService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILocationRepository _locationRepository;

    public DepartmentService(IDepartmentRepository repository, IMapper mapper,
                             ITransactionRepository transactionRepository, ILocationRepository locationRepository,
                             IEmployeeRepository employeeRepository) : base(repository, mapper,
                                                                            transactionRepository)
    {
        _locationRepository = locationRepository;
        _employeeRepository = employeeRepository;
    }

    public override async Task CreateAsync(DepartmentRequestDto request)
    {
        if (request.ManagerId != null)
            await CheckNullReference(request.ManagerId.Value, _employeeRepository, nameof(request.ManagerId));
        await CheckNullReference(request.LocationId, _locationRepository, nameof(request.LocationId));
        await base.CreateAsync(request);
    }

    public override async Task<bool> UpdateAsync(Guid id, DepartmentRequestDto request)
    {
        if (request.ManagerId != null)
            await CheckNullReference(request.ManagerId.Value, _employeeRepository, nameof(request.ManagerId));
        await CheckNullReference(request.LocationId, _locationRepository, nameof(request.LocationId));
        return await base.UpdateAsync(id, request);
    }
}
