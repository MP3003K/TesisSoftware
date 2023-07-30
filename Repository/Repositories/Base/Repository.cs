using Contracts.Repositories.Base;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Repositories.Base;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly DbSet<TEntity> Table;

    protected Repository(ApplicationDbContext dBContext)
    {
        DbContext = dBContext;
        Table = dBContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Table.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<TEntity>> ListAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        Table.Add(entity);

        await DbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Table.Update(entity);

        await DbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            return await DeleteAsync(entity);
        }

        return false;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        Table.Remove(entity);

        await DbContext.SaveChangesAsync();

        return true;
    }
}