using JetBrains.Annotations;
using SE.Abp.Domain.Entities;

namespace SE.Abp.Domain.Repositories;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    [NotNull]
    Task InsertAsync([NotNull] TEntity entity);
    
    Task InsertManyAsync([NotNull] IEnumerable<TEntity> entities);
    
    [NotNull]
    Task UpdateAsync([NotNull] TEntity entity);
    
    Task UpdateManyAsync([NotNull] IEnumerable<TEntity> entities);
    
    Task DeleteAsync([NotNull] TEntity entity);
    
    Task DeleteManyAsync([NotNull] IEnumerable<TEntity> entities);
}

public interface IRepository<TEntity, TKey> : IRepository<TEntity>
    where TEntity : class, IEntity<TKey>
{
    Task<TEntity> GetAsync(TKey id);
    
    Task DeleteAsync(TKey id); 
    
    Task DeleteManyAsync([NotNull] IEnumerable<TKey> ids);
}
