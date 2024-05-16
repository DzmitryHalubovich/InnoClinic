using AutoMapper;
using Profiles.Contracts.DTOs.OuterServicesModels;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;

namespace Profiles.Services.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IDoctorsService> _doctorsService;
    private readonly Lazy<IPatientsService> _patientsService;
    private readonly Lazy<IReceptionistsService> _receptionistsService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, 
        IHttpClientFactory factory, IHttpRepository<OfficeDTO> httpRepository)
    {
        _doctorsService = new Lazy<IDoctorsService>(() => new
            DoctorsService(repositoryManager, mapper, httpRepository));
        _patientsService = new Lazy<IPatientsService>(() => new
            PatientsService(repositoryManager, mapper));
        _receptionistsService = new Lazy<IReceptionistsService>(() => new
            ReceptionistsService(repositoryManager, mapper, httpRepository));
    }

    public IDoctorsService DoctorsService => _doctorsService.Value;

    public IPatientsService PatientsService => _patientsService.Value;

    public IReceptionistsService ReceptionistsService => _receptionistsService.Value;
}
