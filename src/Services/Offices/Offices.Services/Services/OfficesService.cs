using AutoMapper;
using Offices.Contracts.DTOs;
using Offices.Domain.Entities;
using Offices.Domain.Interfaces;
using Offices.Services.Abstractions;
using OneOf;
using OneOf.Types;

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

    public async Task<OneOf<List<OfficeResponseDTO>, NotFound>> GetAllOfficesAsync()
    {
        var offices = await _officesRepository.GetAllAsync();

        if (!offices.Any())
        {
            return new NotFound();
        }

        var mappedOfficesCollection = _mapper.Map<List<OfficeResponseDTO>>(offices);

        return mappedOfficesCollection;
    }

    public async Task<OneOf<List<OfficeResponseDTO>, NotFound>> GetOfficesByIdsAsync(IEnumerable<string> officesIds)
    {
        var offices = await _officesRepository.GetCollectionByIdsAsync(officesIds);

        if (!offices.Any())
        {
            return new NotFound();
        }

        var mappedOfficesCollection = _mapper.Map<List<OfficeResponseDTO>>(offices);

        return mappedOfficesCollection;
    }

    public async Task<OneOf<OfficeResponseDTO, NotFound>> GetOfficeByIdAsync(string officeId)
    {
        var office = await _officesRepository.GetByIdAsync(officeId);

        if (office is null)
        {
            return new NotFound();
        }

        var mappedOffice = _mapper.Map<OfficeResponseDTO>(office);

        return mappedOffice;
    }

    public async Task<OfficeResponseDTO> AddNewOfficeAsync(OfficeCreateDTO newOffice)
    {
        var mappedOffice = _mapper.Map<Office>(newOffice);

        await _officesRepository.AddNewAsync(mappedOffice);

        var createdOffice = _mapper.Map<OfficeResponseDTO>(mappedOffice);

        return createdOffice;
    }

    public async Task<OneOf<Success, NotFound>> DeleteOfficeAsync(string officeId)
    {
        var office = await _officesRepository.GetByIdAsync(officeId);

        if (office is null)
        {
            return new NotFound();
        }

        await _officesRepository.DeleteAsync(officeId);

        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> UpdateOfficeAsync(string officeId, OfficeUpdateDTO updatedOffice)
    {
        var office = await _officesRepository.GetByIdAsync(officeId);

        if (office is null)
        {
            return new NotFound();
        }

        office = _mapper.Map<Office>(updatedOffice);

        await _officesRepository.UpdateAsync(officeId, office);

        return new Success();
    }
}
