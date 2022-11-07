using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesAsync()
    {
        var categories = await _categoryService.FindAsync(c => c.IsActive);
        return Ok(categories);
    }

    [HttpGet("{request}")]
    public async Task<ActionResult<CategoryModel>> GetCategoryById(GetCategoryByIdRequestModel request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _categoryService.FindAsync(c => c.IsActive && c.Id == request.Id);
        if(category is null)
        {
            return NotFound();
        }
        return Ok(category);
    }
}