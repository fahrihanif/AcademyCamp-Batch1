using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class LocationRepository : GeneralRepository<Location>, ILocationRepository
{
    public LocationRepository(EmployeeDbContext context) : base(context) { }
}
