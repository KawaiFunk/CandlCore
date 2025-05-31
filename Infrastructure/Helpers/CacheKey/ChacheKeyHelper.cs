using Domain.Common.PagedList;

namespace Infrastructure.Helpers.CacheKey;

public static class CacheKeyHelper
{
    public static string GetAllAssetsCacheKey(PagedListFilter? filter = null)
        => filter == null ? "assets:all" : $"assets:all:{filter.PageNumber}:{filter.PageSize}:{filter.Descending}";

    public static string GetAssetByIdCacheKey(string id)
        => $"assets:{id}";

    public static string GetAllAssetHistoryCacheKey(PagedListFilter? filter = null)
        => filter == null ? "assetsHistory:all" : $"assetsHistory:all:{filter.PageNumber}:{filter.PageSize}:{filter.Descending}";

    public static string GetAssetHistoryByIdCacheKey(string id)
        => $"assetsHistory:{id}";
}