using System.Linq.Expressions;
using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.Business.Models.Post.Update;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface IPostService
{
    Task<IEnumerable<FindPostResponseModel>> FindAsync(Expression<Func<Post, bool>> filter);
    Task<IEnumerable<FindPostResponseModel>> FindAsyncWithPagination(Expression<Func<Post, bool>> filter, int pageIndex, int size);
    Task<FindPostResponseModel> GetAsync(GetPostByIdRequestModel getPostByIdRequestModel);
    Task<AddPostResponseModel> AddAsync(AddPostRequestModel addPostRequestModel);
    Task<Guid> AddCommentReference(AddCommentReferenceRequestModel id);
    Task<DeletePostResponseModel> DeleteAsync(DeletePostRequestModel deletePostRequestModel);
    Task<UpdatePostResponseModel> UpdateAsync(UpdatePostRequestModel updatePostRequestModel);
    Task<bool> DeleteCommentReference(CommentModel? comment);
    Task<long> CountAsync();
}