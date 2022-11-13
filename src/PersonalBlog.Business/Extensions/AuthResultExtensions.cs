using PersonalBlog.Business.Models.Auth;

namespace PersonalBlog.Business.Extensions;

public static class AuthResultExtensions
{
    public static AuthResult AddErrors(this AuthResult authResult,  params string[] errorMessages)
    {
        authResult.Errors = new();

        for (int i = 0; i < errorMessages.Length; i++)
        {
            authResult.Errors.Add(errorMessages[i]);
        }
        return authResult;
    }

    public static AuthResult AddTokens(this AuthResult authResult, string jwtToken, string refreshToken)
    {
        authResult.Token = jwtToken;
        authResult.RefreshToken = refreshToken;
        authResult.Result = true;
        return authResult;
    }
}