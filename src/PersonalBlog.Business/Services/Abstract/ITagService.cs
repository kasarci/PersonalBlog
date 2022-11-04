using System.Linq.Expressions;
using PersonalBlog.Business.Models.Tag;
using PersonalBlog.Business.Models.Tag.Add;
using PersonalBlog.Business.Models.Tag.Delete;
using PersonalBlog.Business.Models.Tag.Find;
using PersonalBlog.Business.Models.Tag.Update;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface ITagService
{
    Task<IEnumerable<TagModel>> FindAsync(Expression<Func<Tag, bool>> filter);
    Task<TagModel> FindAsync(FindTagByIdRequestModel findTagByIdRequestModel);
    Task<TagModel> FindAsync(FindTagByNameRequestModel findTagByNameRequest);
    Task<AddTagResponseModel> AddAsync(AddTagRequestModel addTagRequestModel);
    Task<DeleteTagResponseModel> DeleteAsync(DeleteTagRequestModel deleteTagRequestModel);
    Task<UpdateTagResponseModel> UpdateAsync(UpdateTagRequestModel updateTagRequestModel);
}