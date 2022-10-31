using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract;
using Polly.Retry;

namespace PersonalBlog.DataAccess.Repositories.Concrete;

public class PostRepository : RepositoryBase<Post>
{
    public PostRepository(IMongoClient mongoClient, string databaseName, AsyncRetryPolicy asyncRetryPolicy) : base(mongoClient, databaseName, asyncRetryPolicy) { }
}