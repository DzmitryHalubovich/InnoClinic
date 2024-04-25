using AutoMapper;
using Offices.Contracts.DTOs;
using Offices.Domain.Entities;
using Offices.Domain.Exceptions;
using Offices.Domain.Interfaces;
using Offices.Services.Abstractions;

namespace Offices.Services.Services;

public class OfficesService : IOfficesService
{
    private readonly IOfficesRepository _officesRepository;
    private readonly IMapper _mapper;

    public OfficesService(IOfficesRepository officesRepository, IMapper mapper)
    {
        _officesRepository = officesRepository;
        _mapper = mapper;
    }

    public async Task<List<OfficeResponseDTO>> GetAllOfficesAsync()
    {
        var offices = await _officesRepository.GetAllAsync();

        var mappedOfficesCollection = _mapper.Map<List<OfficeResponseDTO>>(offices);

        return mappedOfficesCollection;
    }

    public async Task<OfficeResponseDTO> GetOfficeByIdAsync(string officeId)
    {
        var office = await _officesRepository.GetByIdAsync(officeId);

        if (office is null)
            throw new OfficeNotFoundException(officeId);

        var mappedOffice = _mapper.Map<OfficeResponseDTO>(office);

        return mappedOffice;
    }

    public async Task AddNewOfficeAsync(OfficeCreateDTO newOffice)
    {
        var mappedOffice = _mapper.Map<Office>(newOffice);

        await _officesRepository.AddNewAsync(mappedOffice);
    }

    public async Task DeleteOfficeAsync(string officeId)
    {
        await ThrowExceptionIfOfficeDoesntExistsInDatabase(officeId);

        await _officesRepository.DeleteAsync(officeId);
    }

    public async Task UpdateOfficeAsync(string officeId, OfficeUpdateDTO editedOffice)
    {
        await ThrowExceptionIfOfficeDoesntExistsInDatabase(officeId);

        var office = _mapper.Map<Office>(editedOffice);

        office.Id = officeId;

        await _officesRepository.UpdateAsync(officeId, office);
    }


    private async Task ThrowExceptionIfOfficeDoesntExistsInDatabase(string officeId)
    {
        var doesOfficeExists = await _officesRepository.GetByIdAsync(officeId) is not null;

        if (!doesOfficeExists)
            throw new OfficeNotFoundException(officeId);
    }
}
