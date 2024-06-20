using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class ResetPasswordValidator : AbstractValidator<ForgotPasswordRequestDto>
{
    public ResetPasswordValidator()
    {
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
