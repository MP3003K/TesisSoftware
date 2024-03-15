namespace Interfaces.Transactions
{
    public interface IDatabaseTransaction
    {
        void BeginTransaction();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

