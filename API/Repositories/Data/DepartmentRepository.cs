using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class DepartmentRepository : GeneralRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(EmployeeDbContext context) : base(context) { }
}
