using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Models.Tag;
using PersonalBlog.Business.Models.Tag.Add;
using PersonalBlog.Business.Models.Tag.Delete;
using PersonalBlog.Business.Models.Tag.Update;
using PersonalBlog.Business.Services.Abstract;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<IEnumerable<TagModel>>> GetAllAsync()
    {
        var tags = await _tagService.FindAsync(t => t.IsActive);
        return Ok(tags);
    }

    [HttpGet("getOneById/{id}")]
    public async Task<ActionResult<TagModel>> GetOneById(Guid id)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var tag = await _tagService.FindAsync(t => t.IsActive && t.Id == id);

        if(tag is null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpGet("getOneByName/{name}")]
    public async Task<ActionResult<TagModel>> GetOneByName(string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return BadRequest();
        }

        var tag = await _tagService.FindAsync(t => t.IsActive && t.Name == name);

        if (tag is null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<AddTagResponseModel>> AddAsync([FromBody] AddTagRequestModel request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _tagService.AddAsync(request);
        
        return CreatedAtAction(nameof(GetOneById), new {id = result.Id}, result);
    }

    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<UpdateTagResponseModel>> UpdateAsync([FromBody] UpdateTagRequestModel request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _tagService.UpdateAsync(request);

        return result.Succeed ? Ok(result) : NotFound(result);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<DeleteTagResponseModel>> DeleteAsync ([FromBody] DeleteTagRequestModel request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _tagService.DeleteAsync(request);

        return result.Succeed ? Ok(result) : NotFound(result);
    }
}