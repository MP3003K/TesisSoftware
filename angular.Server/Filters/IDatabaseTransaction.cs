namespace angular.Server.Filters
{
    public interface IDatabaseTransaction
    {
        void BeginTransaction();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

