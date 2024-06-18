using API.Models;

namespace API.Repositories.Interfaces;

public interface IUserRepository : IGeneralRepository<User>
{
    Task<bool> IsUserNameExist(string userName);
    Task<User?> CheckUserNameUser(string userName);
}
