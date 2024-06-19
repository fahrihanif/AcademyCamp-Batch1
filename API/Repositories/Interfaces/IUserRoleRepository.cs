using API.Models;

namespace API.Repositories.Interfaces;

public interface IUserRoleRepository : IGeneralRepository<UserRole>
{
    Task<IEnumerable<string>> GetRoleByEmployeeIdAsync(Guid employeeId);
    Task<UserRole?> GetUserRoleIdByEmployeeIdRoleId(Guid employeeId, Guid roleId);
}
