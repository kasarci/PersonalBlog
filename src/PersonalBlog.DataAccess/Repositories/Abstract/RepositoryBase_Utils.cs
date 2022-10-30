using System.Linq.Expressions;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Polly;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryUtils<T> where T : IEntity, new()
{
    public Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
    {
        return RetryAsync(async () =>
        {
            var result = await _collection.FindAsync(_filterBuilder.Empty, new FindOptions<T, T>() 
            {
                Limit = 1
            });
            return result != null;
        });
    }

    public Task<long> CountAsync(Expression<Func<T, bool>> filter)
    {
        return RetryAsync(async () => await _collection.CountDocumentsAsync(filter));
    }
}