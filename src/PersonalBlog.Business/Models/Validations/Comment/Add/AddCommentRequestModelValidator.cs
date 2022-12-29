using FluentValidation;
using PersonalBlog.Business.Models.Comment.Add;

namespace PersonalBlog.Business.Models.Validations.Comment.Add;

public class AddCommentRequestModelValidator : AbstractValidator<AddCommentRequestModel>
{
    public AddCommentRequestModelValidator()
    {
        RuleFor(acrm => acrm.PostId)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty();
            
        RuleFor(acrm => acrm.UserName)
            .MinimumLength(CommentModelValidatorConfiguration.MinimumUsernameLength)
            .WithMessage($"UserName should have minimum {CommentModelValidatorConfiguration.MinimumUsernameLength} characters.")
            .MaximumLength(CommentModelValidatorConfiguration.MaximumUsernameLength)
            .WithMessage($"UserName should have maximum {CommentModelValidatorConfiguration.MaximumUsernameLength} characters.");

        RuleFor(acrm => acrm.Email)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty()
            .EmailAddress();

        RuleFor(acrm => acrm.Content)
            .MinimumLength(CommentModelValidatorConfiguration.MinimumCommentLength)
            .WithMessage($"Comment should have minimum {CommentModelValidatorConfiguration.MinimumCommentLength} characters.")
            .MaximumLength(CommentModelValidatorConfiguration.MaximumCommentLength)
            .WithMessage($"Comment should have maximum {CommentModelValidatorConfiguration.MaximumCommentLength} characters.");
            
    }
}