using FluentValidation;
using PersonalBlog.Business.Models.Tag.Delete;

namespace PersonalBlog.Business.Models.Validations.Tag.Delete;

public class DeleteTagRequestModelValidator : AbstractValidator<DeleteTagRequestModel>
{
    public DeleteTagRequestModelValidator()
    {
        RuleFor(dtrm => dtrm.Id)
            .NotNull()
            .NotEmpty();
    }
}