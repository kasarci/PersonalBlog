using System.Linq.Expressions;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryFind<T> where T : IEntity, new()
{
    public Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter)
    {
        return RetryAsync(async () => 
        {
            var result = await _collection.FindAsync(filter);

            // If there is a problem try to use result.ToEnumerable();
            return result.Current;
        });
    }

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, int number = 0, bool isDescending = false)
    {
        return RetryAsync( async () =>
        {
            var result = await _collection.FindAsync(_filterBuilder.Where(filter), 
                                                        new FindOptions<T, T>   {
                                                                                    Limit = number != 0 ? number : null,
                                                                                    Sort = isDescending ? 
                                                                                            _sortDefinition.Descending(p => p.CreatedAt) :
                                                                                            _sortDefinition.Ascending(p => p.CreatedAt)
                                                                                });
            return result.ToEnumerable();
        });
    }

    public virtual Task<IEnumerable<T>> FindAsyncWithPagination(Expression<Func<T, bool>> filter, int pageIndex, int size)
    {
        return RetryAsync(async () => 
        {
            var result = await _collection.FindAsync(_filterBuilder.Where(filter), new FindOptions<T,T>()
            {
                Skip = (pageIndex - 1) * size,
                Limit = size,
                Sort = _sortDefinition.Descending(x => x.CreatedAt)
            });
            return result.ToEnumerable();
        });
    }

    public virtual Task<IEnumerable<T>> FindAsyncWithPagination(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order, int pageIndex, int size)
    {
        return FindAsyncWithPagination(filter,order,pageIndex,size,true);
    }

    public virtual Task<IEnumerable<T>> FindAsyncWithPagination(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order, int pageIndex, int size, bool isDescending)
    {
        return RetryAsync( async () =>
        {
            var result = await _collection.FindAsync(filter, new FindOptions<T,T>()
            {
                Skip = (pageIndex - 1) * size,
                Limit = size,
                Sort = isDescending ? _sortDefinition.Descending(order) : _sortDefinition.Ascending(order)
            });
            return result.Current;
        });
    }
}