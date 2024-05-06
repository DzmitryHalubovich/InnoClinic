using Profiles.Contracts.DTOs;

namespace Profiles.Services.Abstractions;

public interface IDoctorsService
{
    public Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(bool trackChanges);
    public Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid doctorId, bool trackChanges);
    public Task<DoctorResponseDTO> CreateDoctorAsync(DoctorCreateDTO newDoctor);
}
