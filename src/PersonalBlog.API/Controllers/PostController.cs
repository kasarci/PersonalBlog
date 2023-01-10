using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.API.Settings;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;
using PersonalBlog.Business.Services.Abstract;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize("RequireAdminRole")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private static IConfiguration _configuration;

    public static BlogSettings BlogSettings 
    { 
        get 
        {
            if (_configuration is null)
            {
                throw new ArgumentNullException(nameof(_configuration), "Failed to load configuration in PostController.");
            }
            return _configuration.GetSection(nameof(BlogSettings)).Get<BlogSettings>();
        }
    } 

    public PostController(IPostService postService, IConfiguration configuration)
    {
        _postService = postService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("getCount")]
    [AllowAnonymous]
    public async Task<ActionResult<CountAndPaginationSizeResponseModel>> GetCountAndPaginationSize() 
    {
        var responseModel = new CountAndPaginationSizeResponseModel 
        {
             Count = await _postService.CountAsync(),
             PaginationSize = BlogSettings.PaginationSize
        };

        return Ok(responseModel);
    }

    [HttpGet]
    [Route("getCount/{categoryName}")]
    [AllowAnonymous]
    public async Task<ActionResult<CountAndPaginationSizeResponseModel>> GetCountAndPaginationSizeByCategoryName(string categoryName) 
    {
        if(string.IsNullOrEmpty(categoryName)) 
        {
            await GetCountAndPaginationSize();
        }

        var responseModel = new CountAndPaginationSizeResponseModel 
        {
             Count = await _postService.CountByCategoryNameAsync(categoryName),
             PaginationSize = BlogSettings.PaginationSize
        };

        return Ok(responseModel);
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
[Route("getAll/page/{pageIndex}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<FindPostResponseModel>>> GetAllWithPaginationAsync([FromRoute] int pageIndex)
    {
        var posts = await _postService.FindAsyncWithPagination(p=>p.IsActive, pageIndex, BlogSettings.PaginationSize);
        return Ok(posts);
    }

    [HttpGet]
    [Route("getById/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<FindPostResponseModel>> GetOneByIdAsync( [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var post = await _postService.FindAsync(p => p.IsActive && p.Id == id);
        if(post is null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpGet]
    [Route("getByCategory/{categoryName}/page/{pageIndex}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<FindPostResponseModel>>> GetAllByCategoryWithPaginationAsync([FromRoute] string categoryName, int pageIndex)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var posts = await _postService.FindAsyncWithPagination(p => p.IsActive && p.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower()), pageIndex, BlogSettings.PaginationSize);
        
        if(posts is null)
        {
            return NotFound();
        }
        return Ok(posts);
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