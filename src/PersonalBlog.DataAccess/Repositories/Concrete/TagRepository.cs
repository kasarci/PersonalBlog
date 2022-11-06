using MongoDB.Driver;
using Polly.Retry;
using PersonalBlog.DataAccess.Repositories.Abstract;
using Tag = PersonalBlog.DataAccess.Entities.Concrete.Tag;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Concrete;

public class TagRepository : RepositoryBase<Tag>, ITagRepository
{
    public TagRepository(IMongoClient mongoClient, string databaseName, AsyncRetryPolicy asyncRetryPolicy) : base(mongoClient, databaseName, asyncRetryPolicy) { }
}