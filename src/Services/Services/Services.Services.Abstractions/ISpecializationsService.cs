using Services.Contracts.Specialization;

namespace Services.Services.Abstractions;

public interface ISpecializationsService
{
    public Task<List<SpecializationResponseDTO>> GetAllSpecializationsAsync();

    public Task<SpecializationResponseDTO> GetSpecializationByIdAsync(int id);

    public Task<SpecializationResponseDTO> CreateSpecializationAsync(SpecializationCreateDTO newSpecialization);
}
