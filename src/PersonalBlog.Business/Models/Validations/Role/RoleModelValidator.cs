using FluentValidation;
using PersonalBlog.Business.Models.Role;

namespace PersonalBlog.Business.Models.Validations.Role;

public class RoleModelValidator : AbstractValidator<RoleModel>
{
    public RoleModelValidator()
    {
        RuleFor(rm => rm.Name)
            .NotNull()
            .NotEmpty();
    }
}