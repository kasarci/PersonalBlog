using FluentValidation;
using PersonalBlog.Business.Models.User;

namespace PersonalBlog.Business.Models.Validations.User;

public class LoginUserRequestModelValidator : AbstractValidator<LoginUserRequestModel>
{
    public LoginUserRequestModelValidator()
    {
        RuleFor(lurm => lurm.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        RuleFor(lurm => lurm.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password shoul have minimum 6 characters.");
    }
}