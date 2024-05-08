using Profiles.Contracts.DTOs.Receptionist;

namespace Profiles.Services.Abstractions;

public interface IReceptionistsService
{
    public Task<List<ReceptionistResponseDTO>> GetAllReceptionistsAsync(bool trackChanges);
    public Task<ReceptionistResponseDTO> GetReceptionistByIdAsync(Guid receptionistId, bool trackChanges);
    public Task<ReceptionistResponseDTO> CreateReceptionistAsync(ReceptionistCreateDTO newReceptionist);
    public Task DeleteReceptionistAsync(Guid receptionistId);
    public Task UpdateReceptionistAsync(Guid receptionistId, ReceptionistUpdateDTO updatedReceptionist);
}
