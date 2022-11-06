using FluentValidation;
using PersonalBlog.Business.Models.Post.Update;

namespace PersonalBlog.Business.Models.Validations.Post.Update;'

public class UpdatePostRequestModelValidator : AbstractValidator<UpdatePostRequestModel>
{
    public UpdatePostRequestModelValidator()
    {
        RuleFor(uprm => uprm.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(uprm => uprm.Title)
            .MinimumLength(PostModelValidatorConfiguration.MinimumTitleLength)
            .WithMessage($"Title should have minimum {PostModelValidatorConfiguration.MinimumTitleLength} characters.")
            .MaximumLength(PostModelValidatorConfiguration.MaximumTitleLength)
            .WithMessage($"Title should have maximum {PostModelValidatorConfiguration.MaximumTitleLength} characters.");

        RuleFor(uprm => uprm.Content)
            .MinimumLength(PostModelValidatorConfiguration.MinimumContentLength)
            .WithMessage($"Content should have minimum {PostModelValidatorConfiguration.MinimumContentLength} characters.")
            .MaximumLength(PostModelValidatorConfiguration.MaximumContentLength)
            .WithMessage($"Content should have maximum {PostModelValidatorConfiguration.MaximumContentLength} characters.");

        RuleFor(uprm => uprm.CategoryIds)
            .NotNull()
            .NotEmpty();
    }
}