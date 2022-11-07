using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Abstract;
using Polly.Retry;

namespace PersonalBlog.DataAccess.Repositories.Abstract;

public abstract partial class RepositoryBase<T> : RepositoryBaseCommon<T> where T : IEntity, new()
{
    protected RepositoryBase(IMongoClient mongoClient, IConfiguration configuration) : base(mongoClient, configuration) { }
}