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

    public async Task<RefreshToken> GetAsync(string id)
    {
        return await _repository.GetAsync(Guid.Parse(id));
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        await _repository.UpdateOneAsync(refreshToken);
    }
}