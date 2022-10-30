using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryAdd<T>
where T : IEntity, new()
{
    public Task AddManyAsync(IEnumerable<T> entities) => RetryAsync(async () => await _collection.InsertManyAsync(entities));
    public Task AddOneAsync(T entity) => RetryAsync(async () => await  _collection.InsertOneAsync(entity));
}
