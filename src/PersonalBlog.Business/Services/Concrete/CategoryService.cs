using AutoMapper;
using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Category.Delete;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Models.Category.Update;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;

namespace PersonalBlog.Business.Services.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<AddCategoryResponseModel> AddAsync(AddCategoryRequestModel addCategoryRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(DeleteCategoryRequestModel deleteCategoryRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> FindAsync(FindCategoryByIdRequestModel findCategoryByIdRequestModel)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> FindAsync(FindCategoryByNameRequestModel findCategoryByNameRequest)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateCategoryRequestModel updateCategoryRequestModel)
    {
        throw new NotImplementedException();
    }
}