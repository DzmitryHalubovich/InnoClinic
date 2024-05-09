namespace Profiles.Services.Abstractions;

public interface IServiceManager
{
    public IDoctorsService DoctorsService { get; }

    public IPatientsService PatientsService { get; }

    public IReceptionistsService ReceptionistsService { get; }
}
