using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.Business.Services.Concrete;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repository;
    public RefreshTokenService(IRefreshTokenRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(RefreshToken refreshToken)
    {
        await _repository.AddOneAsync(refreshToken);
    }

    public async Task DeleteByUserName(string? userName)
    {
        var refreshToken = (await _repository.FindAsync(r => r.UserName == userName)).SingleOrDefault();

        await _repository.DeleteAsync(refreshToken);
    }

    public async Task<RefreshToken> FindAsync(string id)
    {
        return (await _repository.FindAsync(x => x.Token == id)).FirstOrDefault();
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        await _repository.UpdateOneAsync(refreshToken);
    }
}