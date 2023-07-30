using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class PixRepository : Repository<Pix>, IPixRepository
{
    public PixRepository(ApplicationDbContext dBContext) : base(dBContext)
    {
    }

    public async Task<bool> HasPixiesByBankIdAsync(int bankId)
    {
        return await Table.AnyAsync(x => x.BankId == bankId);
    }

    public async Task<Pix?> GetByKeyAsync(string key)
    {
        return await Table.FirstOrDefaultAsync(x => x.Key == key);
    }
}