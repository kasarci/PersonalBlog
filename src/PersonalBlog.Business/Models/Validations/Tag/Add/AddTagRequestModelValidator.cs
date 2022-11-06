using FluentValidation;
using PersonalBlog.Business.Models.Tag.Add;

namespace PersonalBlog.Business.Models.Validations.Tag.Add;

public class AddTagRequestModelValidator : AbstractValidator<AddTagRequestModel>
{
    public AddTagRequestModelValidator()
    {
         RuleFor(expression: atrm => atrm.Name)
            .MinimumLength(TagModelValidatorConfiguration.MinimumNameLength)
            .WithMessage($"Tag name should have minimum {TagModelValidatorConfiguration.MinimumNameLength} characters")
            .MaximumLength(TagModelValidatorConfiguration.MaximumNameLength)
            .WithMessage($"Tag name should have maximum {TagModelValidatorConfiguration.MaximumNameLength} characters");
    
    }
}