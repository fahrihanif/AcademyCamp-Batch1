namespace API.Repositories.Interfaces;

public interface ITransactionRepository : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
    void ChangeTrackerClear();
}
