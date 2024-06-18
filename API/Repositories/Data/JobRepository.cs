using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class JobRepository : GeneralRepository<Job>, IJobRepository
{
    public JobRepository(EmployeeDbContext context) : base(context) { }
}
