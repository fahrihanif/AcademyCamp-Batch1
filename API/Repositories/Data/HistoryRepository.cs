using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class HistoryRepository : GeneralRepository<History>, IHistoryRepository
{
    public HistoryRepository(EmployeeDbContext context) : base(context) { }
}
