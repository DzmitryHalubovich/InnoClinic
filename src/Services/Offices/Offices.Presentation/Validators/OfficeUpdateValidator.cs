using FluentValidation;
using Offices.Contracts.DTOs;

namespace Offices.Presentation.Validators;

public class OfficeUpdateValidator : AbstractValidator<OfficeUpdateDTO>
{
    public OfficeUpdateValidator()
    {
        RuleFor(o => o.IsActive)
            .NotEmpty().WithMessage("IsActive field have to be provide with value true or false.");
        RuleFor(o => o.Address)
            .NotEmpty().WithMessage("Field Address have to be provide.")
            .MaximumLength(500).WithMessage("Address field should contain less than 500 simbols.");
        RuleFor(o => o.RegistryPhoneNumber)
            .Matches("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$")
            .WithMessage("Wrong phone format.")
            .NotEmpty().WithMessage("Phone number field can not be empty.");
    }
}
