using AutoMapper;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;

namespace Profiles.Services.Services;

public class PatientsService : IPatientsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public PatientsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientResponseDTO>> GetAllPatientsAsync(bool trackCahanges)
    {
        var patients = await _repositoryManager.PatientsRepository.GetAllAsync(trackCahanges);

        var mappedPatients = _mapper.Map<List<PatientResponseDTO>>(patients);

        return mappedPatients;
    }

    public async Task<PatientResponseDTO> GetPatientByIdAsync(Guid patientId, bool trackChanges)
    {
        var patient = await _repositoryManager.PatientsRepository.GetByIdAsync(patientId, trackChanges);

        var mappedPatient = _mapper.Map<PatientResponseDTO>(patient);

        return mappedPatient;
    }

    public async Task<PatientResponseDTO> CreatePatientAsync(PatientCreateDTO newPatient)
    {
        await _repositoryManager.BeginTransactionAsync();

        var personalInfo = _mapper.Map<PersonalInfo>(newPatient.PersonalInfo);

        var createdPersonalInfo = await _repositoryManager.PersonalInfoRepository
            .AddPersonalInfoAsync(personalInfo);

        var newAccount = new Account()
        {
            Email = newPatient.Email,
            CreatedAt = DateTime.UtcNow,
            PersonalInfoId = createdPersonalInfo.PersonalInfoId,
            UpdatedAt = DateTime.UtcNow
        };

        var createdAccount = await _repositoryManager.AccountsRepository.CreateAsync(newAccount);

        var patientForCreation = _mapper.Map<Patient>(newPatient);

        patientForCreation.AccountId = createdAccount.AccountId;

        var createdPatient = await _repositoryManager.PatientsRepository.CreateAsync(patientForCreation);

        var responsePatient = _mapper.Map<PatientResponseDTO>(createdPatient);

        await _repositoryManager.CommitTransactionAsync();

        return responsePatient;
    }

    public async Task DeletePatientAsync(Guid patientId)
    {
        await _repositoryManager.PatientsRepository.DeleteAsync(patientId);
    }
}
