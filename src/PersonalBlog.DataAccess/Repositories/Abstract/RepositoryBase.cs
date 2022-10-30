using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : RepositoryBaseCommon<T> where T : IEntity, new()
{
    protected RepositoryBase(IMongoClient mongoClient, string databaseName, Polly.Retry.AsyncRetryPolicy asyncRetryPolicy) : base(mongoClient, databaseName, asyncRetryPolicy) { }
}