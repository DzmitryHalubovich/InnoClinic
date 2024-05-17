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
    private readonly ISpecializationsRepository _specializationRepository;
    private readonly IMapper _mapper;

    public SpecializationsService(ISpecializationsRepository specializationsRepository, IMapper mapper)
    {
        _specializationRepository = specializationsRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<List<SpecializationResponseDTO>, NotFound>> GetAllSpecializationsAsync()
    {
        var specializationsList = await _specializationRepository.GetAllAsync();

        if (!specializationsList.Any())
        {
            return new NotFound();
        }

        var mappedSpecializationsList = _mapper.Map<List<SpecializationResponseDTO>>(specializationsList);

        return mappedSpecializationsList;
    }

    public async Task<OneOf<SpecializationResponseDTO, NotFound>> GetSpecializationByIdAsync(int id)
    {
        var specializationEntity = await _specializationRepository.GetByIdAsync(id);

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

/*        var specialization = new Specialization()
        {
            Name = newSpecialization.Name,
            Status = (Status)newSpecialization.Status
        };*/

        await _specializationRepository.CreateAsync(specialization);

        var specializationResult = _mapper.Map<SpecializationResponseDTO>(specialization);

        /*var specializationResult = new SpecializationResponseDTO()
        {
            Id = specialization.Id,
            Name = specialization.Name,
            Status = (int)specialization.Status
        };*/

        return specializationResult;
    }

    public async Task<OneOf<Success, NotFound>> UpdateSpecializationAsync(int id, SpecializationUpdateDTO editedSpecialization)
    {
        var specializationEntity = await _specializationRepository.GetByIdAsync(id);

        if (specializationEntity is null)
        {
            return new NotFound();
        }

        _mapper.Map(editedSpecialization, specializationEntity);

        await _specializationRepository.UpdateAsync(specializationEntity);

        return new Success();
    }
}
