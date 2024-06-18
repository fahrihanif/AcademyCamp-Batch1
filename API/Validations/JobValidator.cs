using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class JobValidator : AbstractValidator<JobRequestDto>
{
    public JobValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(x => x.MinSalary)
           .NotEmpty()
           .LessThanOrEqualTo(x => x.MaxSalary);

        RuleFor(x => x.MaxSalary)
           .NotEmpty()
           .GreaterThanOrEqualTo(x => x.MinSalary);
    }
}
