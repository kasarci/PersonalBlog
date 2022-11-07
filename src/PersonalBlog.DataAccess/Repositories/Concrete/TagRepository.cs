using MongoDB.Driver;
using Polly.Retry;
using PersonalBlog.DataAccess.Repositories.Abstract;
using Tag = PersonalBlog.DataAccess.Entities.Concrete.Tag;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PersonalBlog.DataAccess.Repositories.Concrete;

public class TagRepository : RepositoryBase<Tag>, ITagRepository
{
    public TagRepository(IMongoClient mongoClient, IConfiguration configuration) : base(mongoClient, configuration)
    {
    }
}