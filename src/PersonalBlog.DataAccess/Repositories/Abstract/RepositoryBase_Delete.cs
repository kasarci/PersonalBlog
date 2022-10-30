using System.Linq.Expressions;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryDelete<T>
where T : IEntity, new()
{
    public virtual Task<bool> DeleteAllAsync()
    {
        return RetryAsync(async () =>
        {
            var result = await _collection.DeleteManyAsync(_filterBuilder.Empty);
            return result.IsAcknowledged;
        });
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return RetryAsync(async () =>
        {
            var result = await _collection.DeleteOneAsync(_filterBuilder.Eq(x => x.Id, id));
            return result.IsAcknowledged;
        });
    }

    public Task<bool> DeleteAsync(T entity)
    {
        return DeleteAsync(entity.Id);
    }

    public Task<bool> DeleteAsync(Expression<Func<T, bool>> filter)
    {
        return RetryAsync(async () => 
        {
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        });
    }
}