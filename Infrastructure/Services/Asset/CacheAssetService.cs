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

    public async Task UpsertAsync(AssetEntity entity)
    {
        await _inner.UpsertAsync(entity);
        _cache.Remove(CacheKeyHelper.GetAssetByIdCacheKey(entity.Id.ToString()));
        _cache.Remove(CacheKeyHelper.GetAllAssetsCacheKey());
    }

    public async Task<IPagedList<AssetEntity>> GetAllAsync(PagedListFilter filter)
    {
        var cacheKey = CacheKeyHelper.GetAllAssetsCacheKey(filter);
        if (!_cache.TryGetValue(cacheKey, out IPagedList<AssetEntity> result))
        {
            result = await _inner.GetAllAsync(filter);
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        }

        return result;
    }

    public async Task AddAsync(AssetEntity entity)
    {
        await _inner.AddAsync(entity);
        _cache.Remove(CacheKeyHelper.GetAllAssetsCacheKey());
    }

    public async Task AddRangeAsync(IEnumerable<AssetEntity> entities)
    {
        await _inner.AddRangeAsync(entities);
        _cache.Remove(CacheKeyHelper.GetAllAssetsCacheKey());
    }

    public async Task DeleteAsync(string id)
    {
        await _inner.DeleteAsync(id);
        _cache.Remove(CacheKeyHelper.GetAssetByIdCacheKey(id));
        _cache.Remove(CacheKeyHelper.GetAllAssetsCacheKey());
    }

    public Task<bool> ExistsAsync(string id) => _inner.ExistsAsync(id);

    public Task<AssetEntity> GetByIdAsync(string id) => _inner.GetByIdAsync(id);

    public async Task UpdateAsync(AssetEntity entity)
    {
        await _inner.UpdateAsync(entity);
        _cache.Remove(CacheKeyHelper.GetAssetByIdCacheKey(entity.Id.ToString()));
        _cache.Remove(CacheKeyHelper.GetAllAssetsCacheKey());
    }
}