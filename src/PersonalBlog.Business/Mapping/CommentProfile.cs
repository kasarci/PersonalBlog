using AutoMapper;
using PersonalBlog.Business.Models.Comment;
using PersonalBlog.Business.Models.Comment.Add;
using PersonalBlog.Business.Models.Comment.Delete;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Mapping;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentModel>();
        CreateMap<Comment, AddCommentResponseModel>();
        CreateMap<DeleteCommentRequestModel, Comment>();
        CreateMap<AddCommentRequestModel, Comment>()
            .ForMember(c => c.Id, opt => 
                opt.MapFrom(i => Guid.NewGuid()))
            .ForMember(c => c.CreatedAt, opt => 
                opt.MapFrom(x => DateTimeOffset.UtcNow));
    }
}