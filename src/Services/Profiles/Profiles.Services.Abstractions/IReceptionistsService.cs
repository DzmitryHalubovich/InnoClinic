using OneOf;
using OneOf.Types;
using Profiles.Contracts.DTOs.Receptionist;

namespace Profiles.Services.Abstractions;

public interface IReceptionistsService
{
    public Task<OneOf<List<ReceptionistResponseDTO>, NotFound>> GetAllReceptionistsAsync(bool trackChanges);

    public Task<OneOf<ReceptionistResponseDTO, NotFound>> GetReceptionistByIdAsync(Guid id, bool trackChanges);

    public Task<ReceptionistResponseDTO> CreateReceptionistAsync(ReceptionistCreateDTO newReceptionist);

    public Task<OneOf<Success, NotFound>> UpdateReceptionistAsync(Guid id, ReceptionistUpdateDTO updatedReceptionist);

    public Task<OneOf<Success, NotFound>> DeleteReceptionistAsync(Guid id);
}
