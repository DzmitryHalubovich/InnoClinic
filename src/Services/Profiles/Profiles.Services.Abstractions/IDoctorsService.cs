using Profiles.Contracts.DTOs;

namespace Profiles.Services.Abstractions;

public interface IDoctorsService
{
    public Task<List<DoctorResponseDTO>> GetAllDoctorsAsync();
    public Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid doctorId);
}
