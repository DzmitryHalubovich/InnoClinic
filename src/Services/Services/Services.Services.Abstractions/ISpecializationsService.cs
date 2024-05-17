using OneOf;
using OneOf.Types;
using Services.Contracts.Specialization;

namespace Services.Services.Abstractions;

public interface ISpecializationsService
{
    public Task<OneOf<List<SpecializationResponseDTO>, NotFound>> GetAllSpecializationsAsync();

    public Task<OneOf<SpecializationResponseDTO, NotFound>> GetSpecializationByIdAsync(int id);

    public Task<SpecializationResponseDTO> CreateSpecializationAsync(SpecializationCreateDTO newSpecialization);

    public Task<OneOf<Success, NotFound>> UpdateSpecializationAsync(int id, SpecializationUpdateDTO editedSpecialization);
}
