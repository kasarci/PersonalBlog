using FluentValidation;
using PersonalBlog.Business.Models.Post.Find;

namespace PersonalBlog.Business.Models.Validations.Post.Find;

public class GetPostRequestModelValidator : AbstractValidator<GetPostByIdRequestModel>
{
    public GetPostRequestModelValidator()
    {
        RuleFor(dprm => dprm.Id)
            .NotNull()
            .NotEmpty();
    }
}