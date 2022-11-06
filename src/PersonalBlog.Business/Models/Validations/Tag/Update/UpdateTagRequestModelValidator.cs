using FluentValidation;
using PersonalBlog.Business.Models.Tag.Update;

namespace PersonalBlog.Business.Models.Validations.Tag.Update;

public class UpdateTagRequestModelValidator : AbstractValidator<UpdateTagRequestModel>
{
    public UpdateTagRequestModelValidator()
    {
        RuleFor(ucrm => ucrm.Id)
            .NotNull()
            .NotEmpty();
        
        RuleFor(ucrm => ucrm.Name)
            .MinimumLength(TagModelValidatorConfiguration.MinimumNameLength)
            .WithMessage($"Tag name should have minimum {TagModelValidatorConfiguration.MinimumNameLength} characters")
            .MaximumLength(TagModelValidatorConfiguration.MaximumNameLength)
            .WithMessage($"Tag name should have maximum {TagModelValidatorConfiguration.MaximumNameLength} characters");
    }
}