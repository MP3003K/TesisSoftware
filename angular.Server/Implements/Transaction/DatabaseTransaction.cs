using Repository.Context;
using webapi.Dao.Transactions;

namespace Repository.Transaction;

public class DatabaseTransaction : IDatabaseTransaction
{
    private readonly ApplicationDbContext _dbContext;

    public DatabaseTransaction(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BeginTransaction()
    {
        _dbContext.Database.BeginTransaction();
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}