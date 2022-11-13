using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;
using PersonalBlog.Business.Services.Abstract;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("RequireAdminRole")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    [Route("getAll")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<FindPostResponseModel>>> GetAllAsync()
    {
        var posts = await _postService.FindAsync(p => p.IsActive);
        return Ok(posts);
    }

    [HttpGet]
    [Route("getById/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<FindPostResponseModel>> GetOneByIdAsync( GetPostByIdRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var post = await _postService.FindAsync(p => p.IsActive && p.Id == request.Id);
        if(post is null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<AddPostResponseModel>> AddAsync(AddPostRequestModel request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _postService.AddAsync(request);
        
        return Ok(result);
    }

    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<UpdatePostResponseModel>> UpdateAsync(UpdatePostRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _postService.UpdateAsync(request);

        return result.Succeed ? Ok(result) : NotFound(result);
    }

    
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<DeletePostResponseModel>> DeleteAsync(DeletePostRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _postService.DeleteAsync(request);

        return result.Succeed ? Ok(result) : NotFound(result);
    }

}