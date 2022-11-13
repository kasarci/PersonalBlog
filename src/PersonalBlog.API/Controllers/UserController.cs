using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Models.User.Create;
using PersonalBlog.Business.Models.User.Remove;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;

    public UserController(UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<CreateUserResponseModel>> Create(CreateUserRequestModel user)
    {
        if (ModelState.IsValid)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);

            //Adding admin role to users for test purposes.
            var role = await _roleManager.FindByIdAsync("Admin");
            appUser.AddRole(role.Id);

            var result = await _userManager.CreateAsync(appUser, user.Password);

            if (result.Succeeded)
            {
                return Ok(_mapper.Map<CreateUserResponseModel>(appUser));
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
            }
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("delete")]
    [Authorize("RequireAdminRole")]
    public async Task<ActionResult> Delete(RemoveUserRequestModel user)
    {
        if (ModelState.IsValid)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            if (appUser is null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(appUser);
            if (result.Succeeded)
            {
                return Ok(new { Result = "User has succesfully deleted."});
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
            }
        }
        return BadRequest(ModelState);
    }
}