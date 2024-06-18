using System.Linq.Expressions;
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

    public async Task<(IEnumerable<EmployeeDetailResponseDto> mapEmployeeDetail, int count)> GetEmployeeDetails(EmployeeDetailRequestDto request)
    {
        if (request.PageIndex < 1)
        {
            throw new ArgumentException("'PageIndex can't below than 1'");
        }
        
        var employeeDetails = await _repository.GetDetailAsync() ?? throw new NullReferenceException("Employee detail is empty.");

        if (!string.IsNullOrEmpty(request.Search))
        {
            employeeDetails = employeeDetails.Where(e => e.FirstName.Contains(request.Search) 
                                                      || e.LastName.Contains(request.Search));
        }

        if (request.IsDescending)
        {
            //employeeDetails = employeeDetails.OrderByDescending(GetPropertyValue(request));
        }
        else
        {
            //employeeDetails = employeeDetails.OrderBy(GetPropertyValue(request));
        }

        var employeeCount = employeeDetails.Count();
        employeeDetails = employeeDetails.Skip((request.PageIndex - 1) * request.PageSize)
                                         .Take(request.PageSize);

        var mapEmployeeDetail = _mapper.Map<IEnumerable<EmployeeDetailResponseDto>>(employeeDetails);
        return (mapEmployeeDetail, employeeCount);
    }

    private static Expression<Func<Employee, object>> GetPropertyValue(EmployeeDetailRequestDto request)
    {
        Expression<Func<Employee, object>> keySelector = request.SortColumn?.ToLower()
            switch {
                "fullname" => e => e.GetFullName(),
                "email" => e => e.Email,
                "username" => e => e.User!.UserName,
                "phonenumber" => e => e.PhoneNumber,
                "hiredate" => e => e.HireDate,
                "salary" => e => e.Salary,
                "comissionpct" => e => e.ComissionPct,
                _ => e => e.Nik
            };

        return keySelector;
    }
}
