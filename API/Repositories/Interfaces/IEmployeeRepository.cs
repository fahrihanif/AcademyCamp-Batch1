using API.Models;

namespace API.Repositories.Interfaces;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    Task<string?> GetLastNik();
    Task<bool> IsEmailExist(string email);
    Task<bool> IsPhoneNumberExist(string phoneNumber);
    Task<Employee?> CheckEmailEmployee(string email);
    Task<IEnumerable<Employee>> GetDetailAsync();
}
