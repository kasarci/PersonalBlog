using FluentValidation;
using PersonalBlog.Business.Models.Category.Find;

namespace PersonalBlog.Business.Models.Validations.Category.Find;

public class GetCategoryByIdRequestModelValidator : AbstractValidator<GetCategoryByIdRequestModel>
{
    public GetCategoryByIdRequestModelValidator()
    {
        RuleFor(gcbi => gcbi.Id)
            .NotNull()
            .NotEmpty();
    }
}