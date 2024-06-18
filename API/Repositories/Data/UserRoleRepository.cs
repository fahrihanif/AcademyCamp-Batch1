using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class UserRoleRepository : GeneralRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(EmployeeDbContext context) : base(context) { }
}
