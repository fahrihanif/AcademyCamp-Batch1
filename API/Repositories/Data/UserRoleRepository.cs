using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class UserRoleRepository : GeneralRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(EmployeeDbContext context) : base(context) { }
    public async Task<IEnumerable<string>> GetRoleByEmployeeIdAsync(Guid employeeId)
    {
        return await _context.UserRoles
                             .Where(ur => ur.EmployeeId == employeeId)
                             .Include(ur => ur.Role)
                             .Select(s => s.Role!.Name).ToListAsync();
    }

    public Task<UserRole?> GetUserRoleIdByEmployeeIdRoleId(Guid employeeId, Guid roleId)
    {
        return Task.FromResult(_context.UserRoles.FirstOrDefault(ur => ur.EmployeeId == employeeId && ur.RoleId == roleId));
    }
}
