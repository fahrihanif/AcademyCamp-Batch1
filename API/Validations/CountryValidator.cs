using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class CountryValidator : AbstractValidator<CountryRequestDto>
{
    public CountryValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(40);

        RuleFor(x => x.RegionId)
           .NotEmpty();
    }
}
