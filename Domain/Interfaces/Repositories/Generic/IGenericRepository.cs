using Domain.Common.PagedList;
using Domain.Entities;

namespace Domain.Interfaces.Repositories.Generic;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T>             GetByIdAsync(string          id);
    Task<IPagedList<T>> GetAllAsync(PagedListFilter  filter);
    Task                AddAsync(T                   entity);
    Task                UpdateAsync(T                entity);
    Task                DeleteAsync(string           id);
    Task<bool>          ExistsAsync(string           id);
    Task                AddRangeAsync(IEnumerable<T> entities);
    IQueryable<T>       Table { get; }
}