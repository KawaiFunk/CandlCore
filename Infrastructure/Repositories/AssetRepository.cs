using Domain.Common.PagedList;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Helpers.PagedList;
using Infrastructure.Repositories.Generic;
using MongoDB.Driver.Linq;

namespace Infrastructure.Repositories;

public class AssetRepository(MongoDbContext context) : GenericRepository<AssetEntity>(context), IAssetRepository
{
    public async Task<AssetEntity> GetByExternalIdAsync(string externalId)
    {
        return await Table.FirstOrDefaultAsync(a => a.ExternalId == externalId);
    }

    public new async Task<IPagedList<AssetEntity>> GetAllAsync(PagedListFilter filter)
    {
        var query = Table.Take(300);
        
        if (filter.Search != null)
        {
            query = query.Where(it => it.Name.ToLower().Contains(filter.Search.ToLower()));
        }
        
        if (filter.Descending)
        {
            query = query.OrderByDescending(x => x.Id);
        }
        
        return await query.ToPagedListAsync(filter);
    }
}