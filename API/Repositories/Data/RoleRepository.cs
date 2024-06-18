using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(EmployeeDbContext context) : base(context) { }

    public Task<Guid> GetEmployeeRole()
    {
        var id = _context.Roles.FirstOrDefault(role => role.Name.Contains("Employee"))?.Id;

        if (id is null)
        {
            id = Guid.NewGuid();
            _context.Roles.Add(new Role {
                Id = id.Value,
                Name = "Employee"
            });
            _context.SaveChangesAsync();
        }

        return Task.FromResult(id.Value);
    }
}
