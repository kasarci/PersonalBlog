using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using Polly.Retry;

namespace PersonalBlog.DataAccess.Repositories.Concrete;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoClient mongoClient, IConfiguration configuration) : base(mongoClient, configuration) { }
}