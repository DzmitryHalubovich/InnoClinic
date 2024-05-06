using Profiles.Contracts.DTOs.Patient;

namespace Profiles.Services.Abstractions;

public interface IPatientsService
{
    public Task<IEnumerable<PatientResponseDTO>> GetAllPatientsAsync(bool trackCahanges);
    public Task<PatientResponseDTO> GetPatientByIdAsync(Guid patientId, bool trackChanges);
    public Task<PatientResponseDTO> CreatePatientAsync(PatientCreateDTO newPatient);

    public Task DeletePatientAsync(Guid patientId);
}
