using SE.Domain.Entities;
using SE.Domain.Repositories;

namespace SE.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected RepositoryBase()
    {

    }

    public abstract Task InsertAsync(TEntity entity);

    public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            await InsertAsync(entity);
        }
    }

    public abstract Task UpdateAsync(TEntity entity);

    public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            await UpdateAsync(entity);
        }
    }

    public abstract Task DeleteAsync(TEntity entity);

    public virtual async Task DeleteManyAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            await DeleteAsync(entity);
        }
    }

    public abstract Task<List<TEntity>> GetListAsync();

    public abstract Task<int> GetCountAsync();

    public abstract Task<PageResult<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting);
}

public abstract class RepositoryBase<TEntity, TKey> : RepositoryBase<TEntity>,  IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    public abstract Task<TEntity> GetAsync(TKey id);
    
    public abstract Task DeleteAsync(TKey id);

    public virtual async Task DeleteManyAsync(IEnumerable<TKey> ids)
    {
        foreach (var id in ids)
        {
            await DeleteAsync(id);
        }
    }
}
