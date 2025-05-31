using Domain.Common.PagedList;
using Domain.Entities;
using Domain.Interfaces.Repositories.Generic;
using Infrastructure.Data;
using Infrastructure.Helpers.PagedList;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Repositories.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly MongoDbContext _context;

    public IQueryable<T> Table =>
        _context.GetCollection<T>(typeof(T).Name).AsQueryable();

    public GenericRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        return await Table.FirstOrDefaultAsync(entity => entity.Id == objectId);
    }

    public async Task<IPagedList<T>> GetAllAsync(PagedListFilter filter)
    {
        return await Table.ToPagedListAsync(filter);
    }

    public async Task AddAsync(T entity)
    {
        await _context.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
        await _context.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        await _context.GetCollection<T>(typeof(T).Name).DeleteOneAsync(entity => entity.Id == objectId);
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq(entity => entity.Id, objectId);
        return await _context.GetCollection<T>(typeof(T).Name).Find(filter).AnyAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.GetCollection<T>(typeof(T).Name).InsertManyAsync(entities);
    }
}