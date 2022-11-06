using FluentValidation;
using PersonalBlog.Business.Models.Tag.Find;

namespace PersonalBlog.Business.Models.Validations.Tag.Delete;

public class FindTagByIdRequestModelValidator : AbstractValidator<FindTagByIdRequestModel>
{
    public FindTagByIdRequestModelValidator()
    {
        RuleFor(dtrm => dtrm.Id)
            .NotNull()
            .NotEmpty();
    }
}