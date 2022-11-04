using System.Linq.Expressions;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface IPostService
{
    Task<IEnumerable<FindPostResponseModel>> FindAsync(Expression<Func<Post, bool>> filter);
    Task<FindPostResponseModel> GetAsync(GetPostByIdRequestModel getPostByIdRequestModel);
    Task<AddPostResponseModel> AddAsync(AddPostRequestModel addPostRequestModel);
    Task<DeletePostResponseModel> DeleteAsync(DeletePostRequestModel deletePostRequestModel);
    Task<UpdatePostResponseModel> UpdateAsync(UpdatePostRequestModel updatePostRequestModel);
}