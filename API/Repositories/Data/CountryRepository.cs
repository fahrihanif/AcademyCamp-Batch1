using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class CountryRepository : GeneralRepository<Country>, ICountryRepository
{
    public CountryRepository(EmployeeDbContext context) : base(context) { }
}
