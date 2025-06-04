using Application.Interfaces.Services;
using Domain.Common.PagedList;
using Domain.Entities;
using Infrastructure.Helpers.CacheKey;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services.Asset;

public class CacheAssetService : IAssetService
{
    private readonly IAssetService _inner;
    private readonly IMemoryCache  _cache;

    public CacheAssetService(IAssetService inner, IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<AssetEntity> GetByIdAsync(string id)
    {
        var cacheKey = CacheKeyHelper.GetAssetByIdCacheKey(id);
        if (!_cache.TryGetValue(cacheKey, out AssetEntity result))
        {
            result = await _inner.GetByIdAsync(id);
            if (result != null)
            {
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
            }
        }

        return result;
    }

    public async Task<IPagedList<AssetEntity>> GetAllAsync(PagedListFilter filter)
    {
        //TODO Create cache logic for GetAllAsync with filtering pagination for fixed values
        return await _inner.GetAllAsync(filter);
    }

    public async Task UpsertAsync(AssetEntity entity)
    {
        await _inner.UpsertAsync(entity);
        _cache.Remove(CacheKeyHelper.GetAssetByIdCacheKey(entity.Id.ToString()));
    }

    public async Task AddAsync(AssetEntity entity)
    {
        await _inner.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<AssetEntity> entities)
    {
        await _inner.AddRangeAsync(entities);
    }

    public async Task DeleteAsync(string id)
    {
        await _inner.DeleteAsync(id);
        _cache.Remove(CacheKeyHelper.GetAssetByIdCacheKey(id));
    }

    //TODO Implement cache logic for ExistsAsync if needed
    public Task<bool> ExistsAsync(string id) => _inner.ExistsAsync(id);


    public async Task UpdateAsync(AssetEntity entity)
    {
        await _inner.UpdateAsync(entity);
        _cache.Remove(CacheKeyHelper.GetAssetByIdCacheKey(entity.Id.ToString()));
    }
}