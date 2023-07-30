using Contracts.Repositories.Base;
using Domain.Entities;

namespace Contracts.Repositories;

public interface IBankRepository : IRepository<Bank>
{
    Task<Bank?> GetByCodeAsync(string code);
}