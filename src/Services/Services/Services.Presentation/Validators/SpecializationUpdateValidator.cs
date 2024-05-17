using FluentValidation;
using Services.Contracts.Specialization;

namespace Services.Presentation.Validators;

public class SpecializationUpdateValidator : AbstractValidator<SpecializationUpdateDTO>
{
    public SpecializationUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
    }
}
