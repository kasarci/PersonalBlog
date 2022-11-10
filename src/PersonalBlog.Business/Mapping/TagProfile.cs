using AutoMapper;
using PersonalBlog.Business.Models.Tag;
using PersonalBlog.Business.Models.Tag.Add;
using PersonalBlog.Business.Models.Tag.Delete;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Mapping;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagModel>();
        CreateMap<Tag, AddTagResponseModel>();
        CreateMap<DeleteTagRequestModel, Tag>();
        CreateMap<AddTagRequestModel, Tag>()
            .ForMember(c => c.Id, opt => 
                opt.MapFrom(i => Guid.NewGuid()))
            .ForMember(c => c.CreatedAt, opt => 
                opt.MapFrom(x => DateTimeOffset.UtcNow));
    }
}