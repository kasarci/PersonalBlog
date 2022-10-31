using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract;
using Polly.Retry;

namespace PersonalBlog.DataAccess.Repositories.Concrete;

public class CategoryRepository : RepositoryBase<Category>
{
    public CategoryRepository(IMongoClient mongoClient, string databaseName, AsyncRetryPolicy asyncRetryPolicy) : base(mongoClient, databaseName, asyncRetryPolicy) { }
}