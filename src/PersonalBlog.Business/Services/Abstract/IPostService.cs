using PersonalBlog.Business.Models.Post;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;

namespace PersonalBlog.Business.Services.Abstract;

public interface IPostService
{
    Task<FindPostByIdResponseModel> FindAsync(FindPostByIdRequestModel findPostByIdRequestModel);
    Task<AddPostResponseModel> AddAsync(AddPostRequestModel addPostRequestModel);
    Task DeleteAsync(DeletePostRequestModel deletePostRequestModel);
    Task UpdateAsync(UpdatePostRequestModel updatePostRequestModel);
}