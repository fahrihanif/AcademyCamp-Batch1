using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class DepartmentValidator : AbstractValidator<DepartmentRequestDto>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(30);

        RuleFor(x => x.LocationId)
           .NotNull();
    }
}
