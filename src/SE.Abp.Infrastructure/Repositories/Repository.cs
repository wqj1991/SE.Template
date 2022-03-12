using System.Linq.Expressions;
using Furion.DependencyInjection;
using Mapster;
using SE.Abp.Core;
using SE.Abp.Domain.Entities;
using SE.Abp.Domain.Repositories;
using SE.Abp.Infrastructure.Stores;
using SqlSugar;

namespace SE.Abp.Infrastructure.Repositories;

public class Repository<TEntity, TStore, TKey> : RepositoryBase<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TStore: class, IStore<TKey>, new()
{
    private readonly ISqlSugarRepository<TStore> _sqlSugarRepository;

    public Repository(ISqlSugarRepository<TStore> sqlSugarRepository)
    {
        _sqlSugarRepository = sqlSugarRepository;
    }

    public ISqlSugarRepository<TStore> GetStoreRepository()
    {
        return _sqlSugarRepository;
    }

    public override async Task InsertAsync(TEntity entity)
    {
        await _sqlSugarRepository.InsertAsync(entity.Adapt<TStore>());
    }

    public override async Task UpdateAsync(TEntity entity)
    {
        await _sqlSugarRepository.UpdateAsync(entity.Adapt<TStore>());
    }

    public override async Task DeleteAsync(TEntity entity)
    {
        await _sqlSugarRepository.DeleteAsync(entity.Id);
    }

    public override async Task<List<TEntity>> GetListAsync()
    {
        return (await _sqlSugarRepository.Entities.ToListAsync()).Adapt<List<TEntity>>();
    }

    public override async Task<int> GetCountAsync()
    {
        return await _sqlSugarRepository.CountAsync(e => true);
    }

    public override async Task<PageResult<TEntity>> GetPagedListAsync(int pageSize, int currentPage, string sorting)
    {
        var totalCount = await GetCountAsync();
        var items = await _sqlSugarRepository.Entities.OrderBy(sorting).ToPageListAsync(currentPage, pageSize);
        
        return new PageResult<TEntity>(totalCount, items.Adapt<List<TEntity>>());
    }

    public override async Task<TEntity> GetAsync(TKey id)
    {
        return (await _sqlSugarRepository.FirstOrDefaultAsync(e => e.Id.Equals(id)))?.Adapt<TEntity>();
    }

    public override async Task DeleteAsync(TKey id)
    {
        await _sqlSugarRepository.DeleteAsync(id);
    }
}