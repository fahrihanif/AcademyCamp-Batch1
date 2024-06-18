using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class RegionRepository : GeneralRepository<Region>, IRegionRepository
{
    public RegionRepository(EmployeeDbContext context) : base(context) { }
}
