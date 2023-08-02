using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface IRefreshTokenService 
{
    Task<RefreshToken> FindAsync(string id);
    Task DeleteByUserName(string? userName);
    Task UpdateAsync(RefreshToken refreshToken);
    Task CreateAsync(RefreshToken refreshToken);
}