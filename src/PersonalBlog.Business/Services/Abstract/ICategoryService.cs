using System.Linq.Expressions;
using PersonalBlog.Business.Models.Category;
using PersonalBlog.Business.Models.Category.Add;
using PersonalBlog.Business.Models.Category.Delete;
using PersonalBlog.Business.Models.Category.Find;
using PersonalBlog.Business.Models.Category.Update;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> FindAsync(Expression<Func<Category,bool>> filter);
    Task<CategoryModel> GetAsync(GetCategoryByIdRequestModel getCategoryByIdRequestModel);
    Task<CategoryModel> GetAsync(GetCategoryByNameRequestModel getCategoryByNameRequest);
    Task<AddCategoryResponseModel> AddAsync(AddCategoryRequestModel addCategoryRequestModel);
    Task<DeleteCategoryResponseModel> DeleteAsync(DeleteCategoryRequestModel deleteCategoryRequestModel);
    Task<UpdateCategoryResponseModel> UpdateAsync(UpdateCategoryRequestModel updateCategoryRequestModel);
}