using AutoMapper;
using OneOf;
using OneOf.Types;
using Services.Contracts.Service;
using Services.Contracts.Specialization;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using Services.Services.Abstractions;

namespace Services.Services;

public class ServicesService : IServicesService
{
    private readonly IServicesRepository _servicesRepository;
    private readonly ISpecializationsRepository _specializationsRepository;
    private readonly IServiceCategoryRepository _serviceCategoryRepository;
    private readonly IMapper _mapper;

    public ServicesService(IServicesRepository servicesRepository, ISpecializationsRepository specializationsRepository, 
       IServiceCategoryRepository serviceCategoryRepository, IMapper mapper)
    {
        _servicesRepository = servicesRepository;
        _specializationsRepository = specializationsRepository;
        _serviceCategoryRepository = serviceCategoryRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<List<ServiceResponseDTO>, NotFound>> GetActiveServicesAsync()
    {
        var activeServicesList = await _servicesRepository.GetAllActiveAsync();

        if (!activeServicesList.Any())
        {
            return new NotFound();
        }

        var mappedServicesList = _mapper.Map<List<ServiceResponseDTO>>(activeServicesList);

        return mappedServicesList;
    }

    public async Task<OneOf<ServiceResponseDTO, NotFound>> GetServiceByIdAsync(Guid id)
    {
        var service = await _servicesRepository.GetByIdAsync(id);

        if (service is null)
        {
            return new NotFound();
        }

        var mappedService = _mapper.Map<ServiceResponseDTO>(service);

        return mappedService;
    }
    
    public async Task<OneOf<ServiceResponseDTO, NotFound>> CreateServiceAsync(ServiceCreateDTO newService)
    {
        var specialization = await _specializationsRepository.GetByIdAsync(newService.SpecializationId);

        if (specialization is null)
        {
            return new NotFound();
        }

        var serviceCategory = await _serviceCategoryRepository.GetByIdAsync(newService.ServiceCategoryId);

        if (serviceCategory is null)
        {
            return new NotFound();
        }

        var service = _mapper.Map<Service>(newService);

        await _servicesRepository.CreateAsync(service);

        var serviceResponse = _mapper.Map<ServiceResponseDTO>(service);

        serviceResponse.Specialization = _mapper.Map<SpecializationResponseDTO>(specialization);

        serviceResponse.ServiceCategory = _mapper.Map<ServiceCategoryDTO>(serviceCategory);

        return serviceResponse;
    }
}
