using FluentValidation;
using PersonalBlog.Business.Models.Comment.Delete;

namespace PersonalBlog.Business.Models.Validations.Comment.Delete;

public class DeleteCommentRequestModelValidator : AbstractValidator<DeleteCommentRequestModel>
{
    public DeleteCommentRequestModelValidator()
    {
        RuleFor(dcrm => dcrm.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .NotEmpty();
    }
}