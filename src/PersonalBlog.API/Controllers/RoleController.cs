using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Models.Role;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleController(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateRole(RoleModel roleModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _roleManager.CreateAsync(new ApplicationRole() {Name = roleModel.Name});

            if (result.Succeeded)
            {
                return Ok(new {Result = "Role has successfully created."});
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
    public async Task<ActionResult> DeleteRole(RoleModel roleModel)
    {
        if (ModelState.IsValid)
        {
            var role = await _roleManager.FindByNameAsync(roleModel.Name);

            if (role is null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok(new { Result = "Role has successfully deleted."});
            }
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
        }
        return BadRequest(ModelState);
    }
}