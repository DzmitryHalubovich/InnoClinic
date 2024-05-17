using OneOf;
using OneOf.Types;
using Services.Contracts.Service;

namespace Services.Services.Abstractions;

public interface IServicesService
{
    public Task<OneOf<List<ServiceResponseDTO>, NotFound>> GetActiveServicesAsync();

    public Task<OneOf<ServiceResponseDTO, NotFound>> GetServiceByIdAsync(Guid id);

    public Task<OneOf<ServiceResponseDTO, NotFound>> CreateServiceAsync(ServiceCreateDTO newService);
}
