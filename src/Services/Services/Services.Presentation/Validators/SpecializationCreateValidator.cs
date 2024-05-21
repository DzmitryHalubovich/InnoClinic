using FluentValidation;
using Services.Contracts.Specialization;

namespace Services.Presentation.Validators;

public class SpecializationCreateValidator : AbstractValidator<SpecializationCreateDTO>
{
    public SpecializationCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleForEach(x => x.Services)
            .SetValidator(new ServiceCreateValidator());
    }
}
