using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Comment.Add;
using PersonalBlog.Business.Models.Comment.Delete;

namespace PersonalBlog.Business.Services.Abstract;

public interface ICommentService
{
    Task<AddCommentResponseModel> AddAsync(AddCommentRequestModel addCommentRequestModel);
    Task<DeleteCommentResponseModel> DeleteAsync(DeleteCommentRequestModel deleteCommentRequestModel);
}