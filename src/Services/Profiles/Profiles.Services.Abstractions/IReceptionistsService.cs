using Profiles.Contracts.DTOs.Receptionist;

namespace Profiles.Services.Abstractions;

public interface IReceptionistsService
{
    public Task<List<ReceptionistResponseDTO>> GetAllReceptionistsAsync(bool trackChanges);

    public Task<ReceptionistResponseDTO?> GetReceptionistByIdAsync(Guid id, bool trackChanges);

    public Task<ReceptionistResponseDTO> CreateReceptionistAsync(ReceptionistCreateDTO newReceptionist);

    public Task UpdateReceptionistAsync(Guid id, ReceptionistUpdateDTO updatedReceptionist);

    public Task DeleteReceptionistAsync(Guid id);
}
