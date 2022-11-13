using Microsoft.AspNetCore.Authentication;
using PersonalBlog.Business.Models.Auth;
using PersonalBlog.Business.Models.User;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface IAuthService
{
    Task<AuthResult> VerifyAndGenerateToken(TokenRequestModel tokenRequest);
    Task<AuthResult> GenerateAuthResult(ApplicationUser user);
}