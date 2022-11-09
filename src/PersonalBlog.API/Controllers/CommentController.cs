using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Comment.Add;
using PersonalBlog.Business.Models.Comment.Delete;
using PersonalBlog.Business.Services.Abstract;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{postId}")]
    [Route("getAllByPostId")]
    public async Task<ActionResult<IEnumerable<CommentModel>>> GetAllByPostId(Guid postId)
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

    [HttpGet("{id}")]
    [Route("getOneById")]
    public async Task<ActionResult<CommentModel>> GetOneById(Guid id)
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

        return CreatedAtAction(nameof(GetOneById), new { id = result.Id }, result);
    }

    [HttpDelete]
    [Route("delete")]
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