using FluentValidation;
using PersonalBlog.Business.Models.Post.Delete;

namespace PersonalBlog.Business.Models.Validations.Post.Delete;

public class DeletePostRequestModelValidator : AbstractValidator<DeletePostRequestModel>
{
    public DeletePostRequestModelValidator()
    {
        RuleFor(dprm => dprm.PostId)
            .NotNull()
            .NotEmpty();
    }
}