using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryFindAll<T> where T : IEntity, new()
{
    public Task<IEnumerable<T>> FindAllAsync()
    {
        return RetryAsync(async () => 
        {
            var result= await _collection.FindAsync(_filterBuilder.Empty);
            return result.Current;
        });
    }

    public Task<IEnumerable<T>> FindAllAsyncWithPagination(Expression<Func<T, object>> order, int pageIndex, int size)
    {
        return FindAllAsyncWithPagination(order,pageIndex,size, true);
    }

    public Task<IEnumerable<T>> FindAllAsyncWithPagination(Expression<Func<T, object>> order, int pageIndex, int size, bool isDescending)
    {
        return RetryAsync(async () => 
        {
            var result= await _collection.FindAsync(_filterBuilder.Empty, new FindOptions<T, T>()
            {
                Skip = pageIndex * size,
                Limit = size,
                Sort = isDescending ? _sortDefinition.Descending(order) : _sortDefinition.Ascending(order)
            });
            return result.Current;
        });
    }
}