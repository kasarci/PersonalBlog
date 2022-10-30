using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : IRepositoryGet<T> where T : IEntity, new()
{
    public Task<T> GetAsync(Guid id)
    {
        return RetryAsync(async () => 
        {
            var result = await _collection.FindAsync(x => x.Id == id);
            return result.Current.First();
        });
    }
}