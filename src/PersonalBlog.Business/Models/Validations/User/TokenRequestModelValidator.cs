using FluentValidation;
using PersonalBlog.Business.Models.User;

namespace PersonalBlog.Business.Models.Validations.User;

public class TokenRequestModelValidator : AbstractValidator<TokenRequestModel>
{
    public TokenRequestModelValidator()
    {
        RuleFor(trm => trm.Token)
            .NotNull()
            .NotEmpty();
        RuleFor(trm => trm.RefreshToken)
            .NotNull()
            .NotEmpty();
    }
}