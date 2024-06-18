using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(EmployeeDbContext context) : base(context) { }

    public Task<string?> GetLastNik()
    {
        return Task.FromResult(_context.Employees.OrderBy(employee => employee.Nik).LastOrDefault()?.Nik);
    }

    public Task<bool> IsEmailExist(string email)
    {
        return Task.FromResult(_context.Employees.SingleOrDefault(employee => employee.Email == email) is not null);
    }

    public Task<bool> IsPhoneNumberExist(string phoneNumber)
    {
        return Task.FromResult(_context.Employees.SingleOrDefault(employee =>
                                                                      employee.PhoneNumber.Contains(phoneNumber)) is not
                                   null);
    }

    public Task<Employee?> CheckEmailEmployee(string email)
    {
        return Task.FromResult(_context.Employees.SingleOrDefault(employee => employee.Email.Contains(email)));
    }
}
