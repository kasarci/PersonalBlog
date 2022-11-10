using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Category.Delete;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Models.Category.Update;
using PersonalBlog.Business.Services.Abstract;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesAsync()
    {
        var categories = await _categoryService.FindAsync(c => c.IsActive);
        return Ok(categories);
    }

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<CategoryModel>> GetCategoryById(Guid id)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _categoryService.FindAsync(c => c.IsActive && c.Id == id);
        if(category is null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpGet("getByName/{name}")]
    public async Task<ActionResult<CategoryModel>> GetCategoryByName(string name)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _categoryService.FindAsync(c => c.IsActive && c.Name == name);

        if(category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<AddCategoryResponseModel>> AddCategoryAsync([FromBody] AddCategoryRequestModel request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _categoryService.AddAsync(request);

        return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
    }

    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<UpdateCategoryResponseModel>> UpdateCategoryAsync([FromBody]UpdateCategoryRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _categoryService.UpdateAsync(request);

        return result.Succeed ? Ok(result) : NotFound(result);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<DeleteCategoryResponseModel>> DeleteCategoryAsync([FromBody]DeleteCategoryRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _categoryService.DeleteAsync(request);

        return result.Succeed ? Ok(result) : NotFound(result);
    }
}