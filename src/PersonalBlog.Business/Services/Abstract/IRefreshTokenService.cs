using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface IRefreshTokenService 
{
    Task<RefreshToken> GetAsync(string id);
    Task DeleteByUserName(string? userName);
}