using FluentValidation;
using PersonalBlog.Business.Models.User.Create;

namespace PersonalBlog.Business.Models.Validations.User.Create;

public class CreateUserRequestModelValidator : AbstractValidator<CreateUserRequestModel>
{
    public CreateUserRequestModelValidator()
    {
        RuleFor(curm => curm.UserName)
            .NotNull()
            .NotEmpty();
        RuleFor(curm => curm.Email)
            .NotNull()
            .EmailAddress();
        RuleFor(curm => curm.Password)
            .MinimumLength(6)
            .WithMessage("Password should be more than 6 characters.");
    }
}