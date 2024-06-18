using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class RoleValidator : AbstractValidator<RoleRequestDto>
{
    public RoleValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(50);
    }
}
