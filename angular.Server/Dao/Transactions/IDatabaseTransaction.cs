namespace webapi.Dao.Transactions;

public interface IDatabaseTransaction
{
    void BeginTransaction();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}