using OneOf;
using OneOf.Types;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Contracts.Pagination;

namespace Profiles.Services.Abstractions;

public interface IDoctorsService
{
    public Task<OneOf<List<DoctorResponseDTO>, NotFound>> GetAllDoctorsAsync(DoctorsQueryParameters queryParameters, bool trackChanges);

    public Task<OneOf<DoctorResponseDTO, NotFound>> GetDoctorByIdAsync(Guid id, bool trackChanges);

    public Task<DoctorResponseDTO> CreateDoctorAsync(DoctorCreateDTO newDoctor);
    
    public Task<OneOf<Success, NotFound>> UpdateDoctorAsync(Guid id, DoctorUpdateDTO editedDoctor);

    public Task<OneOf<Success, NotFound>> DeleteDoctorAsync(Guid id);
}
