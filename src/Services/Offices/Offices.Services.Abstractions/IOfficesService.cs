using Offices.Contracts.DTOs;

namespace Offices.Services.Abstractions;

public interface IOfficesService
{
    public Task<List<OfficeResponseDTO>> GetAllOfficesAsync();
    public Task<OfficeResponseDTO> GetOfficeByIdAsync(string officeId);
    public Task<OfficeResponseDTO> AddNewOfficeAsync(OfficeCreateDTO newOffice);
    public Task UpdateOfficeAsync(string officeId, OfficeUpdateDTO editedOffice);
    public Task DeleteOfficeAsync(string officeId);
}
