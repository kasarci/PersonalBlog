using System.Linq.Expressions;
using AutoMapper;
using MongoDB.Driver;
using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Category.Delete;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Models.Category.Update;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.DataAccess.Entities.Concrete;
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

    public async Task<IEnumerable<CategoryModel>> FindAsync(Expression<Func<Category, bool>> filter)
    {
        var result = await _repository.FindAsync(filter);
        return _mapper.Map<IEnumerable<CategoryModel>>(result);
    }

    public async Task<AddCategoryResponseModel> AddAsync(AddCategoryRequestModel addCategoryRequestModel)
    {
        var category = _mapper.Map<Category>(addCategoryRequestModel);
        await _repository.AddOneAsync(category);
        return _mapper.Map<AddCategoryResponseModel>(category);
    }

    public async Task<DeleteCategoryResponseModel> DeleteAsync(DeleteCategoryRequestModel deleteCategoryRequestModel)
    {
        var category = _mapper.Map<Category>(deleteCategoryRequestModel);
        return new DeleteCategoryResponseModel()
        {
            Succeed = await _repository.DeleteAsync(category)
        };
    }

    public async Task<CategoryModel> GetAsync(GetCategoryByIdRequestModel getCategoryByIdRequestModel)
    {
        var result = await _repository.GetAsync(getCategoryByIdRequestModel.Id);
        return _mapper.Map<CategoryModel>(result);
    }

    public async Task<CategoryModel> GetAsync(GetCategoryByNameRequestModel getCategoryByNameRequest)
    {
        var result = (await _repository.FindAsync(c => c.Name == getCategoryByNameRequest.Name)).FirstOrDefault();
        return _mapper.Map<CategoryModel>(result);
    }

    public async Task<UpdateCategoryResponseModel> UpdateAsync(UpdateCategoryRequestModel updateCategoryRequestModel)
    {

        var category = await _repository.GetAsync(updateCategoryRequestModel.Id);
        var updatedCategory = category with { Name = updateCategoryRequestModel.Name };
        
        return new UpdateCategoryResponseModel()
        {
            Succeed = await _repository.UpdateOneAsync(updatedCategory) 
        };
    }
}