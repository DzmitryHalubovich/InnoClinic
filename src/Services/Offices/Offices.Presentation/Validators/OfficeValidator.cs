using FluentValidation;
using FluentValidation.Results;
using Offices.Contracts.DTOs;
using System.Text.RegularExpressions;

namespace Offices.Presentation.Validators;

public class OfficeValidator : AbstractValidator<OfficeCreateDTO>
{
    public OfficeValidator()
    {
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.Photo_Id).NotEmpty();
        RuleFor(x => x.Address).NotEmpty()
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
            .NotEmpty()
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
