using FluentValidation;
using PersonalBlog.Business.Models.User.Remove;

namespace PersonalBlog.Business.Models.Validations.User.Remove;

public class RemoveUserRequestModelValidator : AbstractValidator<RemoveUserRequestModel>
{
    public RemoveUserRequestModelValidator()
    {
        RuleFor(rurm => rurm.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(rurm => rurm.UserName)
            .NotNull()
            .NotEmpty();
    }
}