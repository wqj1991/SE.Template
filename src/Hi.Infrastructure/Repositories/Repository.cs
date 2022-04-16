using Mapster;
using Hi.Domain.Entities;
using Hi.Infrastructure.Dos;
using SqlSugar;

namespace Hi.Infrastructure.Repositories;

public class Repository<TEntity, TDo, TKey> : RepositoryBase<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TDo: class, IDo<TKey>, new()
{
    private readonly ISqlSugarRepository<TDo> _sqlSugarRepository;

    public Repository(ISqlSugarRepository<TDo> sqlSugarRepository)
    {
        _sqlSugarRepository = sqlSugarRepository;
    }

    public ISqlSugarRepository<TDo> GetStoreRepository()
    {
        return _sqlSugarRepository;
    }

    public override async Task InsertAsync(TEntity entity)
    {
        await _sqlSugarRepository.InsertAsync(entity.Adapt<TDo>());
    }

    public override async Task UpdateAsync(TEntity entity)
    {
        await _sqlSugarRepository.UpdateAsync(entity.Adapt<TDo>());
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