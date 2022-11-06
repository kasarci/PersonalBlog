using FluentValidation;
using PersonalBlog.Business.Models.Category.Add;

namespace PersonalBlog.Business.Models.Validations.Category.Add;

public class AddCategoryRequestModelValidator : AbstractValidator<AddCategoryRequestModel>
{
    public AddCategoryRequestModelValidator()
    {
        RuleFor(acrm => acrm.Name)
            .MinimumLength(CategoryModelValidatorConfiguration.MinimumNameLength)
            .WithMessage($"Category name should have minimum {CategoryModelValidatorConfiguration.MinimumNameLength} characters")
            .MaximumLength(CategoryModelValidatorConfiguration.MaximumNameLength)
            .WithMessage($"Category name should have maximum {CategoryModelValidatorConfiguration.MaximumNameLength} characters");
    }
}