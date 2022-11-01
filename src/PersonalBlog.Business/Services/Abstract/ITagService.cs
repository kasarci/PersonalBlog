using PersonalBlog.Business.Models.Tag;
using PersonalBlog.Business.Models.Tag.Add;
using PersonalBlog.Business.Models.Tag.Delete;
using PersonalBlog.Business.Models.Tag.Find;
using PersonalBlog.Business.Models.Tag.Update;

namespace PersonalBlog.Business.Services.Abstract;

public interface ITagService
{
    Task<TagModel> FindAsync(FindTagByIdRequestModel findTagByIdRequestModel);
    Task<TagModel> FindAsync(FindTagByNameRequestModel findTagByNameRequest);
    Task<AddTagResponseModel> AddAsync(AddTagRequestModel addTagRequestModel);
    Task DeleteAsync(DeleteTagRequestModel deleteTagRequestModel);
    Task UpdateAsync(UpdateTagRequestModel updateTagRequestModel);
}