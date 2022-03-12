using System.Linq.Expressions;
using Furion.DependencyInjection;
using SE.Abp.Domain.Entities;
using SE.Abp.Domain.Repositories;
using SqlSugar;

namespace SE.Abp.SqlSugar.Repositories;

public class SqlSugarRepository<TEntity, TStore> : RepositoryBase<TEntity>, IScoped
    where TEntity : class, IEntity
{
    private readonly ISqlSugarRepository<TStore> _sqlSugarRepository

    public override Task<TEntity> InsertAsync(TEntity entity)
    {
        
    }

    public override Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public override Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public override Task<List<TEntity>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public override Task<long> GetCountAsync()
    {
        throw new NotImplementedException();
    }

    public override Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting)
    {
        throw new NotImplementedException();
    }

    public override Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}

public class SqlSugarRepository<TDbContext, TEntity, TKey> : SqlSugarRepository<TDbContext, TEntity>,
    ISqlSugarRepository<TEntity, TKey>,
    ISupportsExplicitLoading<TEntity, TKey>

    where TDbContext : IEfCoreDbContext
    where TEntity : class, IEntity<TKey>
{
    public SqlSugarRepository(IDbContextProvider<TDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    public virtual async Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return includeDetails
            ? await (await WithDetailsAsync()).OrderBy(e => e.Id).FirstOrDefaultAsync(e => e.Id.Equals(id), GetCancellationToken(cancellationToken))
            : await (await GetDbSetAsync()).FindAsync(new object[] { id }, GetCancellationToken(cancellationToken));
    }

    public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, cancellationToken: cancellationToken);
        if (entity == null)
        {
            return;
        }

        await DeleteAsync(entity, autoSave, cancellationToken);
    }

    public virtual async Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var entities = await (await GetDbSetAsync()).Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

        await DeleteManyAsync(entities, autoSave, cancellationToken);
    }
}
