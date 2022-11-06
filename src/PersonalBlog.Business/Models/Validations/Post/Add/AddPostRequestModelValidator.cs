using FluentValidation;
using PersonalBlog.Business.Models.Post.Add;

namespace PersonalBlog.Business.Models.Validations.Post.Add;

public class AddPostRequestModelValidator : AbstractValidator<AddPostRequestModel>
{
    public AddPostRequestModelValidator()
    {
        RuleFor(aprm => aprm.WriterId)
            .NotNull()
            .NotEmpty();

        RuleFor(aprm => aprm.Title)
            .MinimumLength(PostModelValidatorConfiguration.MinimumTitleLength)
            .WithMessage($"Title should have minimum {PostModelValidatorConfiguration.MinimumTitleLength} characters.")
            .MaximumLength(PostModelValidatorConfiguration.MaximumTitleLength)
            .WithMessage($"Title should have maximum {PostModelValidatorConfiguration.MaximumTitleLength} characters.");

        RuleFor(aprm => aprm.Content)
            .MinimumLength(PostModelValidatorConfiguration.MinimumContentLength)
            .WithMessage($"Content should have minimum {PostModelValidatorConfiguration.MinimumContentLength} characters.")
            .MaximumLength(PostModelValidatorConfiguration.MaximumContentLength)
            .WithMessage($"Content should have maximum {PostModelValidatorConfiguration.MaximumContentLength} characters.");

        RuleFor(aprm => aprm.CategoryIds)
            .NotNull()
            .NotEmpty();
    }
}