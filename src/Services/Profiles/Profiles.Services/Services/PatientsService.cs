using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Domain.Entities;
using Profiles.Domain.Exceptions;
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

    public async Task<List<PatientResponseDTO>> GetAllPatientsAsync(bool trackCahanges)
    {
        var patients = await _repositoryManager.PatientsRepository.GetAllAsync(trackCahanges);

        if (patients.IsNullOrEmpty())
        {
            throw new NotFoundException("There are not patients in the database.");
        }

        var mappedPatients = _mapper.Map<List<PatientResponseDTO>>(patients);

        return mappedPatients;
    }

    public async Task<PatientResponseDTO> GetPatientByIdAsync(Guid patientId, bool trackChanges)
    {
        var patient = await _repositoryManager.PatientsRepository.GetByIdAsync(patientId, trackChanges);

        if (patient is null)
        {
            throw new NotFoundException($"The patient with id: {patientId} was not found in the database.");
        }

        var mappedPatient = _mapper.Map<PatientResponseDTO>(patient);

        return mappedPatient;
    }

    public async Task<PatientResponseDTO> CreatePatientAsync(PatientCreateDTO newPatient)
    {
        var personalInfo = _mapper.Map<PersonalInfo>(newPatient.PersonalInfo);

        var newAccount = new Account()
        {
            PersonalInfo = personalInfo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Email = newPatient.Email
        };

        _repositoryManager.AccountsRepository.Create(newAccount);

        var newPatientEntity = new Patient()
        {
            Account = newAccount,
        };

        _repositoryManager.PatientsRepository.Create(newPatientEntity);

        var patientResult = _mapper.Map<PatientResponseDTO>(newPatientEntity);

        await _repositoryManager.SaveAsync();

        return patientResult;
    }

    public async Task DeletePatientAsync(Guid patientId)
    {
        var patientEntity = await _repositoryManager.PatientsRepository.GetByIdAsync(patientId, true);

        if (patientEntity is null)
        {
            throw new Exception();
        }

        _repositoryManager.PatientsRepository.Delete(patientEntity);
        await _repositoryManager.SaveAsync();
    }
}
