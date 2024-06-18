using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.Utilities;
using AutoMapper;

namespace API.Services.Data;

public class EmployeeService : GeneralService<IEmployeeRepository, EmployeeRequestDto, EmployeeResponseDto, Employee>,
                               IEmployeeService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IJobRepository _jobRepository;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper,
                           ITransactionRepository transactionRepository, IJobRepository jobRepository,
                           IDepartmentRepository departmentRepository) : base(repository, mapper,
                                                                              transactionRepository)
    {
        _jobRepository = jobRepository;
        _departmentRepository = departmentRepository;
    }

    public override async Task CreateAsync(EmployeeRequestDto request)
    {
        if (request.ManagerId != null)
            await CheckNullReference(request.ManagerId.Value, _repository, nameof(request.ManagerId));
        await CheckNullReference(request.DepartmentId, _departmentRepository, nameof(request.DepartmentId));
        await CheckNullReference(request.JobId, _jobRepository, nameof(request.JobId));

        if (await _repository.IsEmailExist(request.Email)) throw new ArgumentException("'Email' already registered.");

        if (await _repository.IsPhoneNumberExist(request.PhoneNumber))
            throw new ArgumentException("'PhoneNumber' already registered.");

        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job != null && (request.Salary < job.MinSalary || request.Salary > job.MaxSalary))
            throw new
                ArgumentException($"'Salary' cannot be lower than {job.MinSalary} and greater than {job.MaxSalary}.");

        var mapEntity = _mapper.Map<Employee>(request);
        mapEntity.Nik = GenerateHandler.Nik(await _repository.GetLastNik());

        await _repository.CreateAsync(mapEntity);
        await _transactionRepository.SaveChangesAsync();
    }

    public override async Task<bool> UpdateAsync(Guid id, EmployeeRequestDto request)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null) return false;

        if (request.ManagerId != null)
            await CheckNullReference(request.ManagerId.Value, _repository, nameof(request.ManagerId));
        await CheckNullReference(request.DepartmentId, _departmentRepository, nameof(request.DepartmentId));
        await CheckNullReference(request.JobId, _jobRepository, nameof(request.JobId));

        if (await _repository.IsEmailExist(request.Email) && request.Email != entity.Email)
            throw new ArgumentException("'Email' already registered.");

        if (await _repository.IsPhoneNumberExist(request.PhoneNumber) && request.PhoneNumber != entity.PhoneNumber)
            throw new ArgumentException("'PhoneNumber' already registered.");

        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job != null && (request.Salary < job.MinSalary || request.Salary > job.MaxSalary))
            throw new
                ArgumentException($"'Salary' cannot be lower than {job.MinSalary} and greater than {job.MaxSalary}.");

        entity = _mapper.Map(request, entity);
        _transactionRepository.ChangeTrackerClear();

        _repository.Update(entity);
        await _transactionRepository.SaveChangesAsync();

        return true;
    }
}
