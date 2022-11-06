using FluentValidation;
using PersonalBlog.Business.Models.Tag.Find;

namespace PersonalBlog.Business.Models.Validations.Tag.Find;

public class GetTagByNameRequestModelValidator : AbstractValidator<FindTagByNameRequestModel>
{
    public GetTagByNameRequestModelValidator()
    {
        RuleFor(ftbn => ftbn.Name)
            .MinimumLength(TagModelValidatorConfiguration.MinimumNameLength)
            .WithMessage($"Tag name should have minimum {TagModelValidatorConfiguration.MinimumNameLength} characters")
            .MaximumLength(TagModelValidatorConfiguration.MaximumNameLength)
            .WithMessage($"Tag name should have maximum {TagModelValidatorConfiguration.MaximumNameLength} characters");
    }
}