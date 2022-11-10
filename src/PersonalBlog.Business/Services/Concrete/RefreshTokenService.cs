using AutoMapper;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.Business.Services.Concrete;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repository;
    private readonly IMapper _mapper;
    public RefreshTokenService(IRefreshTokenRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task DeleteByUserName(string? userName)
    {
        var refreshToken = (await _repository.FindAsync(r => r.UserName == userName)).SingleOrDefault();

        _repository.DeleteAsync(refreshToken);
    }

    public async Task<RefreshToken> GetAsync(string id)
    {
        return await _repository.GetAsync(Guid.Parse(id));
    }
}