using System.Resources;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Comment.Add;
using PersonalBlog.Business.Models.Comment.Delete;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Services.Abstract;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IPostService _postService;

    public CommentController(ICommentService commentService, IPostService postService)
    {
        _commentService = commentService;
        _postService = postService;
    }

    [HttpGet]
    [Route("getAllByPostId")]
    public async Task<ActionResult<IEnumerable<CommentModel>>> GetAllByPostId([FromQuery]Guid postId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comments = await _commentService.FindAsync(c => c.PostId == postId);

        if (comments is null)
        {
            return NotFound();
        }
        return Ok(comments);
    }

    [HttpGet]
    [Route("getOneById")]
    public async Task<ActionResult<CommentModel>> GetOneById([FromQuery] Guid id)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comment = await _commentService.FindAsync(c => c.Id == id);
        if(comment is null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<AddCommentResponseModel>> AddAsync([FromBody] AddCommentRequestModel request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _commentService.AddAsync(request);
        var postResult = await _postService.AddCommentReference(new AddCommentReferenceRequestModel{CommentId = result.Id, PostId = request.PostId});

        return CreatedAtAction(nameof(GetOneById), new { id = result.Id }, result);
    }

    [HttpDelete]
    [Route("delete")]
    [Authorize("RequireAdminRole")]
    public async Task<ActionResult<DeleteCommentResponseModel>> DeleteAsync([FromBody] DeleteCommentRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _commentService.DeleteAsync(request);
        
        return result.Succeed ? Ok(result) : NotFound(result);
    }
}