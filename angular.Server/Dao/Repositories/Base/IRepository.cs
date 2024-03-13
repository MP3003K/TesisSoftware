namespace webapi.Dao.Repositories.Base;

public interface IRepository<TEntity>
{
    public Task<TEntity?> GetByIdAsync(int id);
    public Task<IList<TEntity>> ListAllAsync();
    public Task<TEntity> InsertAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<bool> DeleteByIdAsync(int id);
    public Task<bool> DeleteAsync(TEntity entity);
}