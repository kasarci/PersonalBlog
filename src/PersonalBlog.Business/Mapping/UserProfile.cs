using AutoMapper;
using PersonalBlog.Business.Models.User.Create;
using PersonalBlog.Business.Models.User.Remove;
using PersonalBlog.DataAccess.Entities.Concrete;

namespace PersonalBlog.Business.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequestModel, ApplicationUser>().ReverseMap();
        CreateMap<ApplicationUser, CreateUserResponseModel>().ReverseMap();

        CreateMap<RemoveUserRequestModel, ApplicationUser>().ReverseMap();
    }
}