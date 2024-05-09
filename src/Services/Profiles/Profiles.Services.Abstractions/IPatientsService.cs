using Profiles.Contracts.DTOs.Patient;
using Profiles.Contracts.Pagination;

namespace Profiles.Services.Abstractions;

public interface IPatientsService
{
    public Task<List<PatientResponseDTO>> GetAllPatientsAsync(PatientsQueryParameters queryParameters, bool trackCahanges);

    public Task<PatientResponseDTO?> GetPatientByIdAsync(Guid id, bool trackChanges);

    public Task<PatientResponseDTO> CreatePatientAsync(PatientCreateDTO newPatient);

    public Task UpdatePatientAsync(Guid id, PatientUpdateDTO updatedPatient);

    public Task DeletePatientAsync(Guid id);
}
