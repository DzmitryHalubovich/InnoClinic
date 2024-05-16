using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using OneOf.Types;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Contracts.DTOs.OuterServicesModels;
using Profiles.Contracts.Pagination;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;

namespace Profiles.Services.Services;

public class DoctorsService : IDoctorsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IHttpRepository<OfficeDTO> _httpRepository;

    public DoctorsService(IRepositoryManager repositoryManager,
        IMapper mapper, IHttpRepository<OfficeDTO> httpRepository)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _httpRepository = httpRepository;
    }

    public async Task<OneOf<List<DoctorResponseDTO>, NotFound>> GetAllDoctorsAsync(DoctorsQueryParameters queryParameters, bool trackChanges)
    {
        var doctors = await _repositoryManager.DoctorsRepository
            .GetAllAsync(queryParameters, trackChanges);

        if (doctors.IsNullOrEmpty())
        {
            return new NotFound();
        }

        var mappedDoctors = _mapper.Map<List<DoctorResponseDTO>>(doctors);

        IEnumerable<string> officesIds = doctors.Select(x => x.OfficeId).Distinct().ToList();

        var offices = await _httpRepository.GetCollection(officesIds);

        foreach (var doctor in mappedDoctors)
        {
            doctor.Office = offices.First(x => x.OfficeId.Equals(doctor.Office.OfficeId));
        }

        return mappedDoctors;
    }

    public async Task<OneOf<DoctorResponseDTO, NotFound>> GetDoctorByIdAsync(Guid id, bool trackChanges)
    {
        var doctor = await _repositoryManager.DoctorsRepository.GetByIdAsync(id, trackChanges);

        if (doctor is null)
        {
            return new NotFound();
        }

        var mappedDoctor = _mapper.Map<DoctorResponseDTO>(doctor);

        mappedDoctor.Office = 
            await _httpRepository.GetOneAsync("https://localhost:7255/api/offices", doctor.OfficeId!);

        return mappedDoctor;
    }
    
    public async Task<DoctorResponseDTO> CreateDoctorAsync(DoctorCreateDTO newDoctor)
    {
        var newAccount = new Account()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Email = newDoctor.Email,
        };

        _repositoryManager.AccountsRepository.Create(newAccount);

        var status = _mapper.Map<Status>(newDoctor.Status);

        var newDoctorEntity = _mapper.Map<Doctor>(newDoctor);

        newDoctorEntity.Account = newAccount;
        newDoctorEntity.Status = status;

        var doctorResult = _mapper.Map<DoctorResponseDTO>(newDoctorEntity);

        _repositoryManager.DoctorsRepository.Create(newDoctorEntity);

        await _repositoryManager.SaveAsync();

        doctorResult.Office = await _httpRepository.GetOneAsync("https://localhost:7255/api/offices", newDoctorEntity.OfficeId!);
            
        return doctorResult;
    }

    public async Task<OneOf<Success, NotFound>> UpdateDoctorAsync(Guid id, DoctorUpdateDTO updatedDoctor)
    {
        var doctorEntity = await _repositoryManager.DoctorsRepository.GetByIdAsync(id, true);

        if (doctorEntity is null)
        {
            return new NotFound();
        }

        _mapper.Map(updatedDoctor, doctorEntity);

        await _repositoryManager.SaveAsync();

        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> DeleteDoctorAsync(Guid id)
    {
        var doctorEntity = await _repositoryManager.DoctorsRepository.GetByIdAsync(id, true);

        if (doctorEntity is null)
        {
            return new NotFound();
        }

        _repositoryManager.AccountsRepository.Delete(doctorEntity.Account);

        await _repositoryManager.SaveAsync();

        return new Success();
    }
}
