using API.DTOs.Requests;
using FluentValidation;

namespace API.Validations;

public class LocationValidator : AbstractValidator<LocationRequestDto>
{
    public LocationValidator()
    {
        RuleFor(x => x.StreetAddress)
           .NotEmpty()
           .MaximumLength(40);

        RuleFor(x => x.PostalCode)
           .NotEmpty()
           .MaximumLength(12)
           .Matches(@"^\d+$").WithMessage("Postal code only accept number.");

        RuleFor(x => x.StateProvince)
           .NotEmpty()
           .MaximumLength(25);

        RuleFor(x => x.City)
           .NotEmpty()
           .MaximumLength(30);

        RuleFor(x => x.CountryId)
           .NotEmpty();
    }
}
