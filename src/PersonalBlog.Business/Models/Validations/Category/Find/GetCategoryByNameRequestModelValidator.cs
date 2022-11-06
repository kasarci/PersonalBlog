using FluentValidation;
using PersonalBlog.Business.Models.Category.Find;

namespace PersonalBlog.Business.Models.Validations.Category.Find;

public class GetCategoryByNameRequestModelValidator : AbstractValidator<GetCategoryByNameRequestModel>
{
    public GetCategoryByNameRequestModelValidator()
    {
        RuleFor(gcbn => gcbn.Name)
            .MinimumLength(CategoryModelValidatorConfiguration.MinimumNameLength)
            .WithMessage($"Category name should have minimum {CategoryModelValidatorConfiguration.MinimumNameLength} characters")
            .MaximumLength(CategoryModelValidatorConfiguration.MaximumNameLength)
            .WithMessage($"Category name should have maximum {CategoryModelValidatorConfiguration.MaximumNameLength} characters");
    }
}