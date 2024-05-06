using Profiles.Contracts.DTOs;
using Profiles.Presentation.Pagination;

namespace Profiles.Services.Abstractions;

public interface IDoctorsService
{
    public Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(DoctorsQueryParameters parameters, bool trackChanges);
    public Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid doctorId, bool trackChanges);
    public Task<DoctorResponseDTO> CreateDoctorAsync(DoctorCreateDTO newDoctor);
}
