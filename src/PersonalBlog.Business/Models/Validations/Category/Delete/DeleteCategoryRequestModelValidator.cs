using FluentValidation;
using PersonalBlog.Business.Models.Category.Delete;

namespace PersonalBlog.Business.Models.Validations.Category.Delete;

public class DeleteCategoryRequestModelValidator : AbstractValidator<DeleteCategoryRequestModel>
{
    public DeleteCategoryRequestModelValidator()
    {
        RuleFor(dcrm => dcrm.Id)
            .NotNull()
            .NotEmpty();
    }
}