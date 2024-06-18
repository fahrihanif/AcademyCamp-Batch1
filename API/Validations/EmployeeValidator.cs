using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class EmployeeValidator : AbstractValidator<EmployeeRequestDto>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.FirstName)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(x => x.LastName)
           .NotNull()
           .MaximumLength(50);

        RuleFor(x => x.Email)
           .NotEmpty()
           .EmailAddress()
           .MaximumLength(50);

        RuleFor(x => x.Salary)
           .NotEmpty();

        RuleFor(x => x.PhoneNumber)
           .NotEmpty()
           .Matches(@"^\d+$").WithMessage("Phone number only accept number.")
           .MaximumLength(20);

        RuleFor(x => x.ComissionPct)
           .NotNull()
           .PrecisionScale(3, 2, false);

        RuleFor(x => x.JobId)
           .NotEmpty();

        RuleFor(x => x.DepartmentId)
           .NotEmpty();
    }
}
