using Contracts.Repositories.Base;
using Domain.Entities;

namespace Contracts.Repositories;

public interface IPixRepository : IRepository<Pix>
{
    Task<bool> HasPixiesByBankIdAsync(int bankId);

    Task<Pix?> GetByKeyAsync(string key);
}