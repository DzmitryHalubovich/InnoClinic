using AutoMapper;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;

namespace Profiles.Services.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IDoctorsService> _doctorsService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IHttpClientFactory factory)
    {
        _doctorsService = new Lazy<IDoctorsService>(() => new
            DoctorsService(repositoryManager, mapper, factory));
    }

    public IDoctorsService DoctorsService => _doctorsService.Value;
}
