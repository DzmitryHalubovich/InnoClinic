using AutoMapper;
using OneOf;
using OneOf.Types;
using Services.Contracts.Specialization;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using Services.Services.Abstractions;

namespace Services.Services;

public class SpecializationsService : ISpecializationsService
{
    private readonly ISpecializationsRepository _specializationsRepository;
    private readonly IMapper _mapper;

    public SpecializationsService(ISpecializationsRepository specializationsRepository, IMapper mapper)
    {
        _specializationsRepository = specializationsRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<List<SpecializationResponseDTO>, NotFound>> GetAllSpecializationsAsync()
    {
        var specializationsList = await _specializationsRepository.GetAllAsync();

        if (!specializationsList.Any())
        {
            return new NotFound();
        }

        var mappedSpecializationsList = _mapper.Map<List<SpecializationResponseDTO>>(specializationsList);

        return mappedSpecializationsList;
    }

    public async Task<OneOf<SpecializationResponseDTO, NotFound>> GetSpecializationByIdAsync(int id)
    {
        var specializationEntity = await _specializationsRepository.GetByIdAsync(id);

        if (specializationEntity is null)
        {
            return new NotFound();
        }

        var mappedSpecialization = _mapper.Map<SpecializationResponseDTO>(specializationEntity);

        return mappedSpecialization;
    }

    public async Task<SpecializationResponseDTO> CreateSpecializationAsync(SpecializationCreateDTO newSpecialization)
    {
        var specialization = _mapper.Map<Specialization>(newSpecialization);

        await _specializationsRepository.CreateAsync(specialization);

        var specializationResult = _mapper.Map<SpecializationResponseDTO>(specialization);

        return specializationResult;
    }

    public async Task<OneOf<Success, NotFound>> UpdateSpecializationAsync(int id, SpecializationUpdateDTO editedSpecialization)
    {
        var specializationEntity = await _specializationsRepository.GetByIdAsync(id);

        if (specializationEntity is null)
        {
            return new NotFound();
        }

        _mapper.Map(editedSpecialization, specializationEntity);

        await _specializationsRepository.UpdateAsync(specializationEntity);

        return new Success();
    }
}
