using AutoMapper;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;

namespace Profiles.Services.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IDoctorsService> _doctorsService;
    private readonly Lazy<IPatientsService> _patientsService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IHttpClientFactory factory)
    {
        _doctorsService = new Lazy<IDoctorsService>(() => new
            DoctorsService(repositoryManager, mapper, factory));
        _patientsService = new Lazy<IPatientsService>(() => new
            PatientsService(repositoryManager, mapper));
    }

    public IDoctorsService DoctorsService => _doctorsService.Value;
    public IPatientsService PatientsService => _patientsService.Value;
}
