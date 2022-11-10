using AutoMapper;
using PersonalBlog.Business.Models.Post.Add;
using PersonalBlog.Business.Models.Post.Delete;
using PersonalBlog.Business.Models.Post.Find;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, FindPostResponseModel>();
        CreateMap<Post, AddPostResponseModel>();
        CreateMap<DeletePostRequestModel, Post>();
        CreateMap<AddPostRequestModel, Post>()
            .ForMember(c => c.Id, opt => 
                opt.MapFrom(i => Guid.NewGuid()))
            .ForMember(c => c.CreatedAt, opt => 
                opt.MapFrom(x => DateTimeOffset.UtcNow));
    }
}