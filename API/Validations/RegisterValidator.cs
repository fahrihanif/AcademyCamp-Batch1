using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
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

        RuleFor(x => x.UserName)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(x => x.Password)
           .NotEmpty()
           .MinimumLength(8)
           .Matches("[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
           .Matches("[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
           .Matches("[0-9]+").WithMessage("Password must contain at least one number.")
           .Matches(@"[\!\?\*\.]+").WithMessage("Password must contain at least one (!? *.).");

        RuleFor(x => x.ConfirmPassword)
           .NotEmpty()
           .Equal(user => user.Password).WithMessage("'Confirm Password' is not the same with 'Password'");
    }
}
