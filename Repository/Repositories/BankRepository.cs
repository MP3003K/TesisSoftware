using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class BankRepository : Repository<Bank>, IBankRepository
{
    public BankRepository(ApplicationDbContext dBContext) : base(dBContext)
    {
    }

    public async Task<Bank?> GetByCodeAsync(string code)
    {
        return await Table.FirstOrDefaultAsync(x => x.Code == code);
    }
}