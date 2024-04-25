using FluentValidation;
using FluentValidation.Results;
using Offices.Contracts.DTOs;
using System.Text.RegularExpressions;

namespace Offices.Presentation.Validators;

public class OfficeUpdateValidator : AbstractValidator<OfficeUpdateDTO>
{
    public OfficeUpdateValidator()
    {
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Field Address is empty.")
            .MaximumLength(500).WithMessage("Address fiel is too long.")
            .Custom((name, context) =>
            {
                Regex rg = new Regex("<.*?>"); // Matches HTML tags
                if (rg.Matches(name).Count > 0)
                {
                    // Raises an error
                    context.AddFailure(
                    new ValidationFailure(
                    "Name",
                    "The parameter has invalid content"
                    ));
                }
            });
        RuleFor(x => x.Registry_phone_number)
            .Matches("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$")
            .WithMessage("Wrong phone format.")
            .NotEmpty().WithMessage("Registry phone number is empty.")
            .Custom((name, context) =>
            {
                Regex rg = new Regex("<.*?>"); // Matches HTML tags
                if (rg.Matches(name).Count > 0)
                {
                    // Raises an error
                    context.AddFailure(
                    new ValidationFailure(
                    "Name",
                    "The parameter has invalid content"
                    ));
                }
            });
    }
}
