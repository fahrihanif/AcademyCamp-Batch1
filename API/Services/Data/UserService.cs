using System.Security.Claims;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.Utilities;
using AutoMapper;

namespace API.Services.Data;

public class UserService : GeneralService<IUserRepository, UserRequestDto, UserResponseDto, User>, IUserService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ITokenHandler _tokenHandler;
    private readonly IEmailHandler _emailHandler;

    public UserService(IUserRepository repository, IMapper mapper, ITransactionRepository transactionRepository,
                       IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,
                       IJobRepository jobRepository, IUserRoleRepository userRoleRepository,
                       IRoleRepository roleRepository, ITokenHandler tokenHandler, IEmailHandler emailHandler) :
        base(repository, mapper, transactionRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _jobRepository = jobRepository;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
        _tokenHandler = tokenHandler;
        _emailHandler = emailHandler;
    }

    public async Task<string> LoginUserAsync(LoginRequestDto request)
    {
        var employee = await _employeeRepository.CheckEmailEmployee(request.EmailOrUsername);
        _transactionRepository.ChangeTrackerClear();
        var user = await _repository.CheckUserNameUser(request.EmailOrUsername);
        _transactionRepository.ChangeTrackerClear();
        if (user == null && employee is null)
            throw new NullReferenceException("Email/UserName and Password is not found");
        if (user == null)
            user = await _repository.GetByIdAsync(employee.Id);
        else
            employee = await _employeeRepository.GetByIdAsync(user.EmployeeId);

        if (!HashPasswordHandler.VerifyPassword(request.Password, user!.Password))
            throw new NullReferenceException("Email/UserName and Password is not found");

        var claims = new List<Claim> {
            new("nik", employee.Nik),
            new("name", employee.GetFullName()),
            new("email", employee.Email)
        };
        var userRole = await _userRoleRepository.GetRoleByEmployeeIdAsync(employee.Id);
        claims.AddRange(userRole.Select(item => new Claim(ClaimTypes.Role, item)));
        var token = _tokenHandler.Access(claims);
        
        return token;
    }

    public async Task AddUserRoleAsync(UserRoleRequestDto requestDto)
    {
        await CheckNullReference(requestDto.EmployeeId, _employeeRepository, nameof(requestDto.EmployeeId));
        await CheckNullReference(requestDto.RoleId, _roleRepository, nameof(requestDto.RoleId));

        var toEntity = _mapper.Map<UserRole>(requestDto);
        await _userRoleRepository.CreateAsync(toEntity);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task RemoveUserRoleAsync(UserRoleRequestDto requestDto)
    {
        await CheckNullReference(requestDto.EmployeeId, _employeeRepository, nameof(requestDto.EmployeeId));
        await CheckNullReference(requestDto.RoleId, _roleRepository, nameof(requestDto.RoleId));

        var userRole = await _userRoleRepository.GetUserRoleIdByEmployeeIdRoleId(requestDto.EmployeeId, requestDto.RoleId);
        if (userRole is null)
        {
            throw new NullReferenceException("User did not have the role.");
        }
        
        _userRoleRepository.Delete(userRole);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task GenerateOtpAsync(GenerateOtpRequestDto requestDto)
    {
        var employee = await _employeeRepository.CheckEmailEmployee(requestDto.Email);
        if (employee is null)
            throw new NullReferenceException("Account not found.");

        var user = await _repository.GetByIdAsync(employee.Id);
        if (user is null)
            throw new NullReferenceException("Account not found.");

        user.Otp = new Random().Next(111111, 999999);
        user.IsOtpUsed = false;
        user.ExpiredOtp = DateTime.Now.AddMinutes(5);

        await _emailHandler.SendEmailAsync(new EmailRequestDto(employee.Email, employee.GetFullName(), "OTP - Academy Camp", $"Here is your otp: <b>{user.Otp}</b>"));
        
        _repository.Update(user);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequestDto requestDto)
    {
        var employee = await _employeeRepository.CheckEmailEmployee(requestDto.EmailOrUsername);
        _transactionRepository.ChangeTrackerClear();
        var user = await _repository.CheckUserNameUser(requestDto.EmailOrUsername);
        _transactionRepository.ChangeTrackerClear();
        if (user is null && employee is null)
            throw new NullReferenceException("Email/UserName and Password is not found");
        if (user is null)
            user = await _repository.GetByIdAsync(employee!.Id);

        if (user.Otp != requestDto.Otp)
            throw new ArgumentException("'Otp' is incorrect.");
        if (user.ExpiredOtp > DateTime.Now )
            throw new ArgumentException("'Otp' has expired.");
        if (user!.IsOtpUsed)
            throw new ArgumentException("'Otp' has been already used");
        
        user.IsOtpUsed = true;
        user.Password = HashPasswordHandler.GenerateHash(requestDto.Password);
        _repository.Update(user);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task RegisterUserAsync(RegisterRequestDto request)
    {
        try
        {
            await _transactionRepository.BeginTransactionAsync();

            if (request.ManagerId != null)
                await CheckNullReference(request.ManagerId.Value, _repository, nameof(request.ManagerId));

            await CheckNullReference(request.DepartmentId, _departmentRepository, nameof(request.DepartmentId));
            await CheckNullReference(request.JobId, _jobRepository, nameof(request.JobId));

            if (await _employeeRepository.IsEmailExist(request.Email))
                throw new ArgumentException("'Email' already registered.");

            if (await _employeeRepository.IsPhoneNumberExist(request.PhoneNumber))
                throw new ArgumentException("'PhoneNumber' already registered.");

            if (await _repository.IsUserNameExist(request.UserName))
                throw new ArgumentException("'UserName' already registered.");

            var job = await _jobRepository.GetByIdAsync(request.JobId);
            _transactionRepository.ChangeTrackerClear();
            if (job != null && (request.Salary < job.MinSalary || request.Salary > job.MaxSalary))
                throw new ArgumentException($"'Salary' cannot be lower than {job.MinSalary} and greater than {job.MaxSalary}.");

            var mapEmployee = _mapper.Map<Employee>(request);
            mapEmployee.Nik = GenerateHandler.Nik(await _employeeRepository.GetLastNik());

            await _employeeRepository.CreateAsync(mapEmployee);
            await _transactionRepository.SaveChangesAsync();

            var mapUser = _mapper.Map<User>(request);
            mapUser.EmployeeId = mapEmployee.Id;

            await _repository.CreateAsync(mapUser);
            await _transactionRepository.SaveChangesAsync();

            var getRoleId = await _roleRepository.GetEmployeeRole();
            await _userRoleRepository.CreateAsync(new UserRole {
                Id = Guid.NewGuid(),
                EmployeeId = mapEmployee.Id,
                RoleId = getRoleId
            });
            await _transactionRepository.SaveChangesAsync();

            await _transactionRepository.CommitAsync();
        }
        catch
        {
            await _transactionRepository.RollbackAsync();
            throw;
        }
        finally
        {
            _transactionRepository.Dispose();
        }
    }

    public override async Task CreateAsync(UserRequestDto request)
    {
        if (await _repository.IsUserNameExist(request.UserName))
            throw new ArgumentException("'UserName' already registered.");

        await CheckNullReference(request.EmployeeId, _employeeRepository, nameof(request.EmployeeId));
        await base.CreateAsync(request);
    }

    public override async Task<bool> UpdateAsync(Guid id, UserRequestDto request)
    {
        var user = await _repository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();

        if (user is null) return false;

        if (await _repository.IsUserNameExist(request.UserName) && request.UserName != user.UserName)
            throw new ArgumentException("'UserName' already registered.");

        await CheckNullReference(request.EmployeeId, _employeeRepository, nameof(request.EmployeeId));

        user = _mapper.Map(request, user);

        _repository.Update(user);
        await _transactionRepository.SaveChangesAsync();

        return true;
    }
}
