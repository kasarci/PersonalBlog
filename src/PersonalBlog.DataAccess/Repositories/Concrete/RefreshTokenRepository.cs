using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.DataAccess.Repositories.Concrete;

public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(IMongoClient mongoClient, IConfiguration configuration) : base(mongoClient, configuration)
    {
    }
}