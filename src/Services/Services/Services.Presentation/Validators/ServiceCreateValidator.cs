using FluentValidation;
using Services.Contracts.Service;

namespace Services.Presentation.Validators;

public class ServiceCreateValidator : AbstractValidator<ServiceCreateDTO>
{
    public ServiceCreateValidator()
    {
        
    }
}
