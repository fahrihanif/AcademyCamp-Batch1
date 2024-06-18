using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class UserRepository : GeneralRepository<User>, IUserRepository
{
    public UserRepository(EmployeeDbContext context) : base(context) { }

    public async Task<bool> IsUserNameExist(string userName)
    {
        return await _context.Users.SingleOrDefaultAsync(user => user.UserName == userName) is not null;
    }

    public async Task<User?> CheckUserNameUser(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.UserName.Contains(userName));
    }
}
