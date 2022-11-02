using System.Linq.Expressions;
using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Comment.Add;
using PersonalBlog.Business.Models.Comment.Delete;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Services.Abstract;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> FindAsync(Expression<Func<Comment, bool>> filter);
    Task<AddCommentResponseModel> AddAsync(AddCommentRequestModel addCommentRequestModel);
    Task<DeleteCommentResponseModel> DeleteAsync(DeleteCommentRequestModel deleteCommentRequestModel);
}