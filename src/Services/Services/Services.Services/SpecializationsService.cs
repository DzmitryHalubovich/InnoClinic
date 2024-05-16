using AutoMapper;
using Services.Contracts.Specialization;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using Services.Services.Abstractions;

namespace Services.Services;

public class SpecializationsService : ISpecializationsService
{
    private readonly ISpecializationsRepository _specializationRepository;
    private readonly IMapper _mapper;

    public SpecializationsService(ISpecializationsRepository specializationsRepository, IMapper mapper)
    {
        _specializationRepository = specializationsRepository;
        _mapper = mapper;
    }

    public async Task<List<SpecializationResponseDTO>> GetAllSpecializationsAsync()
    {
        var specializationList = await _specializationRepository.GetAllAsync();

        var mappedspecializationList = _mapper.Map<List<SpecializationResponseDTO>>(specializationList);

        return mappedspecializationList;
    }

    public async Task<SpecializationResponseDTO> GetSpecializationByIdAsync(int id)
    {
        var specializationEntity = await _specializationRepository.GetByIdAsync(id);

        var mappedSpecialization = _mapper.Map<SpecializationResponseDTO>(specializationEntity);

        return mappedSpecialization;
    }

    public async Task<SpecializationResponseDTO> CreateSpecializationAsync(SpecializationCreateDTO newSpecialization)
    {
        var specialization = new Specialization()
        {
            Name = newSpecialization.Name,
            Status = (Status)newSpecialization.Status
        };

        await _specializationRepository.CreateAsync(specialization);

        var specializationResult = new SpecializationResponseDTO()
        {
            Id = specialization.Id,
            Name = specialization.Name,
            Status = (int)specialization.Status
        };

        return specializationResult;
    }
}
