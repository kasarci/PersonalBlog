using System.Linq.Expressions;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryFirst<T> where T : IEntity, new()
{
    public Task<T> FirstAsync()
    {
        return RetryAsync(async () => 
        {
            var result = await _collection.FindAsync(_filterBuilder.Empty, new FindOptions<T, T>() 
            {
                Limit = 1
            });
            return result.Current.First();
        });
    }

    public Task<T> FirstAsync(FilterDefinition<T> filter)
    {
        return RetryAsync(async () => 
        {
            var result = await _collection.FindAsync(filter, new FindOptions<T, T>()
            {
                Limit = 1
            });
            return result.Current.First();
        });
    }

    public Task<T> FirstAsync(Expression<Func<T, bool>> filter)
    {
        return RetryAsync(async () => 
        {
            var result = await _collection.FindAsync(filter, new FindOptions<T, T>()
            {
                Limit = 1
            });
            return result.Current.First();
        });
    }
    
    public Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order)
    {
        return FirstAsync(filter,order,true);
    }

    public Task<T> FirstAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order, bool isDescending)
    {
        return RetryAsync(async () =>
        {
            var result = await _collection.FindAsync(filter, new FindOptions<T, T>()
            {
                Limit = 1,
                Sort = isDescending ? _sortDefinition.Descending(order) : _sortDefinition.Ascending(order)
            });
            return result.Current.First();
        });
    }
}