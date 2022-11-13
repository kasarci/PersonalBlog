using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Extensions;
using PersonalBlog.Business.Models.Auth;
using PersonalBlog.Business.Models.User;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly ILogger<AuthController> _logger;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthService _authService;

    public AuthController(UserManager<ApplicationUser> userManager, ILogger<AuthController> logger, IRefreshTokenRepository refreshTokenRepository, IAuthService authService)
    {
        _userManager = userManager;
        _logger = logger;
        _refreshTokenRepository = refreshTokenRepository;
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResult>> Login([FromBody] LoginUserRequestModel loginUserRequest)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(loginUserRequest.Email);

            if (existingUser is null)
            {
                return BadRequest(new AuthResult().AddErrors("Invalid user"));
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(existingUser, loginUserRequest.Password);

            if(!isPasswordCorrect)
            {
                return BadRequest(new AuthResult().AddErrors("Invalid password"));
            }

            var roles = await _userManager.GetRolesAsync(existingUser);
            _logger.LogInformation($"[{loginUserRequest.Email}] logged in the system with the { String.Join(",", roles)} roles.");

            return Ok(await _authService.GenerateAuthResult(existingUser));
        }

        return BadRequest(new AuthResult().AddErrors("Invalid payload"));
    }

    [HttpPost]
    [Route("refreshToken")]
    public async Task<ActionResult<AuthResult>> RefreshToken([FromBody] TokenRequestModel tokenRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.VerifyAndGenerateToken(tokenRequest);
            
            if (result is null)
            {
                return BadRequest(new AuthResult().AddErrors("Invalid tokens."));
            } 
            return Ok(result);
        }
        return BadRequest(new AuthResult().AddErrors("Invalid parameters."));
    }

}