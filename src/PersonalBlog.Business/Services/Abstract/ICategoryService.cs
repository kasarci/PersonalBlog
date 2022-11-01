using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Category.Delete;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Models.Category.Update;

namespace PersonalBlog.Business.Services.Abstract;

public interface ICategoryService
{
    Task<CategoryModel> FindAsync(FindCategoryByIdRequestModel findCategoryByIdRequestModel);
    Task<CategoryModel> FindAsync(FindCategoryByNameRequestModel findCategoryByNameRequest);
    Task<AddCategoryResponseModel> AddAsync(AddCategoryRequestModel addCategoryRequestModel);
    Task DeleteAsync(DeleteCategoryRequestModel deleteCategoryRequestModel);
    Task UpdateAsync(UpdateCategoryRequestModel updateCategoryRequestModel);
}