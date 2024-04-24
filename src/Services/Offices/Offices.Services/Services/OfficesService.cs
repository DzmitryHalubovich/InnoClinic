using AutoMapper;
using Offices.Contracts.DTOs;
using Offices.Domain.Entities;
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

    public async Task AddNewOfficeAsync(OfficeCreateDTO newOffice)
    {
        var mappedOffice = _mapper.Map<Office>(newOffice);

        await _officesRepository.AddNewAsync(mappedOffice);
    }

    public async Task<List<OfficeResponseDTO>> GetAllOfficesAsync()
    {
        var offices = await _officesRepository.GetAllAsync();

        var officesResult = _mapper.Map<List<OfficeResponseDTO>>(offices);

        return officesResult;
    }
}
