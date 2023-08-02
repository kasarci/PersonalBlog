using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DnsClient.Protocol;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.Business.Extensions;
using PersonalBlog.Business.Models.Auth;
using PersonalBlog.Business.Models.User;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly IRefreshTokenService _refreshTokenService;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public AuthService(IRefreshTokenService refreshTokenService, UserManager<ApplicationUser> userManager, IConfiguration configuration, TokenValidationParameters tokenValidationParameters)
    {
        _refreshTokenService = refreshTokenService;
        _userManager = userManager;
        _configuration = configuration;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public async Task<AuthResult> VerifyAndGenerateToken(TokenRequestModel tokenRequest)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        try
        {
            _tokenValidationParameters.ValidateLifetime = false; //for testing.
            var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out SecurityToken validatedToken);

            //Checking if the token algorithm is HmacSHA256.
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                if(result is false)
                {
                    return null;
                }
            }

            //Checking if the token expired.
            var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

            if (expiryDate < DateTime.UtcNow)
            {
                Console.WriteLine("Access token is expired!");

                // Mistake here. We should not return error from the method if the access token is expired.
                // We need to check if the refresh token is valid first.
                //return new AuthResult().AddErrors("Expired token.");
            }

            //Check if token stored in the database.
            var storedToken = await _refreshTokenService.FindAsync(tokenRequest.RefreshToken);

            if(storedToken is null)
            {
                return new AuthResult().AddErrors("Invalid token.");
            }

            if (storedToken.IsUsed)
            {
                return new AuthResult().AddErrors("Invalid token.");
            }

            if (storedToken.IsRevoked)
            {
                return new AuthResult().AddErrors("Invalid token.");
            }

            // Chekck if the IDs are the same.
            var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);

            if (storedToken.JwtId != jti.Value)
            {
                return new AuthResult().AddErrors("Invalid token.");
            }

            if (storedToken.ExpiryDate < DateTime.UtcNow)
            {
                Console.WriteLine("Refrsh token is expired as well.");
                return new AuthResult().AddErrors("Expired token.");
            }

            var usedToken = storedToken with {IsUsed = true};
            await _refreshTokenService.UpdateAsync(usedToken);

            var dbUser = await _userManager.FindByIdAsync(usedToken.UserId.ToString());

            return await GenerateAuthResult(dbUser);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new AuthResult().AddErrors("Server error.");
        }
    }

    public async Task<AuthResult> GenerateAuthResult(ApplicationUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        byte[]? key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
        
        var authClaims = new ClaimsIdentity(new []
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("username", user.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
        });

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            authClaims.AddClaim(new Claim(ClaimTypes.Role, userRole));
        }

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = authClaims,
            Expires = DateTime.Now.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var tokenString = jwtTokenHandler.WriteToken(token);

        var refreshToken = new RefreshToken()
        {
            JwtId = token.Id,
            Token = RandomStringGeneration(23),
            CreatedAt = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6),
            IsRevoked = false,
            IsUsed = false,
            UserId = user.Id,
            UserName = user.UserName
        };

        await _refreshTokenService.CreateAsync(refreshToken);

        return new AuthResult().AddTokens(tokenString, refreshToken.Token);
    }

    private string RandomStringGeneration(int length)
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_()";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(chars.Length)]).ToArray());
    }

    private DateTime UnixTimeStampToDateTime(long utcExpiryDate)
    {
        var dateTime = new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc);
        return dateTime.AddSeconds(utcExpiryDate).ToUniversalTime();    
    }
}