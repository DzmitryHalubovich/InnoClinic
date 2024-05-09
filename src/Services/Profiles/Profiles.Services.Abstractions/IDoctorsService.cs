using Profiles.Contracts.DTOs.Doctor;
using Profiles.Contracts.Pagination;

namespace Profiles.Services.Abstractions;

public interface IDoctorsService
{
    public Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(DoctorsQueryParameters queryParameters, bool trackChanges);

    public Task<DoctorResponseDTO?> GetDoctorByIdAsync(Guid id, bool trackChanges);

    public Task<DoctorResponseDTO> CreateDoctorAsync(DoctorCreateDTO newDoctor);
    
    public Task UpdateDoctorAsync(Guid id, DoctorUpdateDTO editedDoctor);

    public Task DeleteDoctorAsync(Guid id);
}
