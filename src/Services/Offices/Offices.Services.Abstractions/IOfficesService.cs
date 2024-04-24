using Offices.Contracts.DTOs;

namespace Offices.Services.Abstractions;

public interface IOfficesService
{
    public Task<List<OfficeResponseDTO>> GetAllOfficesAsync();
}
