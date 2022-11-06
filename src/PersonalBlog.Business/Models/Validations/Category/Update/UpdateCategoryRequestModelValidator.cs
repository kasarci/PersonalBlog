using FluentValidation;
using PersonalBlog.Business.Models.Category.Update;

namespace PersonalBlog.Business.Models.Validations.Category.Update;

public class UpdateCategoryRequestModelValidator : AbstractValidator<UpdateCategoryRequestModel>
{
    public UpdateCategoryRequestModelValidator()
    {
        RuleFor(ucrm => ucrm.Id)
            .NotNull()
            .NotEmpty();
        
        RuleFor(ucrm => ucrm.Name)
            .MinimumLength(CategoryModelValidatorConfiguration.MinimumNameLength)
            .WithMessage($"Category name should have minimum {CategoryModelValidatorConfiguration.MinimumNameLength} characters")
            .MaximumLength(CategoryModelValidatorConfiguration.MaximumNameLength)
            .WithMessage($"Category name should have maximum {CategoryModelValidatorConfiguration.MaximumNameLength} characters");
    }
}