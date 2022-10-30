using System.Linq.Expressions;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Polly.Caching;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryUpdate<T> where T : IEntity, new()
{
    public Task<T> FindOneAndUpdateAsync(Expression<Func<T,bool>> filter, UpdateDefinition<T> updateDefinition, FindOneAndUpdateOptions<T>? options)
    {
        return RetryAsync(async () => await _collection.FindOneAndUpdateAsync(filter, updateDefinition, options));
    }

    public Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition, FindOneAndUpdateOptions<T>? options)
    {
        return RetryAsync(async () => await _collection.FindOneAndUpdateAsync(filter,updateDefinition,options));
    }

    public Task<bool> UpdateManyAsync(FilterDefinition<T> filter, params UpdateDefinition<T>[] updateDefinitions)
    {
        return RetryAsync(async () =>
        {
            var updater = _updateBuilder.Combine(updateDefinitions).CurrentDate(x => x.ModifiedAt);
            var result = await _collection.UpdateManyAsync(filter,updater.CurrentDate(x => x.ModifiedAt));
            return result.IsAcknowledged;
        });
    }

    public Task<bool> UpdateManyAsync(Expression<Func<T, bool>> filter, params UpdateDefinition<T>[] updateDefinitions)
    {
        return RetryAsync(async () =>
        {
            var updater = _updateBuilder.Combine(updateDefinitions).CurrentDate(x => x.ModifiedAt);
            var result = await _collection.UpdateManyAsync(filter,updater.CurrentDate(x => x.ModifiedAt));
            return result.IsAcknowledged;
        });
    }

    public Task<bool> UpdateOneAsync(T entity)
    {
        return RetryAsync(async () =>
        {
            entity.ModifiedAt = DateTime.UtcNow;
            var result = await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
            return result.IsAcknowledged;
        });
    }

    public Task<bool> UpdateOneAsync(Guid id, params UpdateDefinition<T>[] updateDefinitions)
    {
        return RetryAsync(async () =>
        {
            var updater = _updateBuilder.Combine(updateDefinitions).CurrentDate(x => x.ModifiedAt);
            var result = await _collection.UpdateManyAsync(x => x.Id == id, updater.CurrentDate(x => x.ModifiedAt));
            return result.IsAcknowledged;
        });
    }

    public Task<bool> UpdateOneAsync(T entity, params UpdateDefinition<T>[] updateDefinitions)
    {
    return RetryAsync(async () =>
        {
            var updater = _updateBuilder.Combine(updateDefinitions).CurrentDate(x => x.ModifiedAt);
            var result = await _collection.UpdateManyAsync(x => x.Id == entity.Id, updater.CurrentDate(x => x.ModifiedAt));
            return result.IsAcknowledged;
        });
    }
}