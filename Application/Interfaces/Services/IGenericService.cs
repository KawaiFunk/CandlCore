using Application.Models.Assets;
using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IGenericService<T> where T : BaseEntity
{
    Task<T>              GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task                 AddAsync(T                   entity);
    Task                 UpdateAsync(T                entity);
    Task                 DeleteAsync(string           id);
    Task<bool>           ExistsAsync(string           id);
    Task                 AddRangeAsync(IEnumerable<T> entities);
}