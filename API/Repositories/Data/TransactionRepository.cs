using API.Data;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Repositories.Data;

public class TransactionRepository : ITransactionRepository
{
    private readonly EmployeeDbContext _context;
    private IDbContextTransaction _transaction = null!;

    public TransactionRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void ChangeTrackerClear()
    {
        _context.ChangeTracker.Clear();
    }
}
