using Contracts.Repositories;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class TransactionRepository : Repository<Domain.Entities.Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext dBContext) : base(dBContext)
    {
    }
}