using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class RegionValidator : AbstractValidator<RegionRequestDto>
{
    public RegionValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(25);
    }
}
