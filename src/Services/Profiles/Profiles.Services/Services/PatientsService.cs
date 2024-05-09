using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Contracts.Pagination;
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

    public async Task<List<PatientResponseDTO>> GetAllPatientsAsync(PatientsQueryParameters queryParameters, bool trackCahanges)
    {
        var patients = await _repositoryManager.PatientsRepository.GetAllAsync(queryParameters, trackCahanges);

        if (patients.IsNullOrEmpty())
        {
            throw new NotFoundException("There are not patients in the database.");
        }

        var mappedPatients = _mapper.Map<List<PatientResponseDTO>>(patients);

        return mappedPatients;
    }

    public async Task<PatientResponseDTO?> GetPatientByIdAsync(Guid id, bool trackChanges)
    {
        var patient = await _repositoryManager.PatientsRepository.GetByIdAsync(id, trackChanges);

        if (patient is null)
        {
            throw new NotFoundException($"The patient with id: {id} was not found in the database.");
        }

        var mappedPatient = _mapper.Map<PatientResponseDTO>(patient);

        return mappedPatient;
    }

    public async Task<PatientResponseDTO> CreatePatientAsync(PatientCreateDTO newPatient)
    {
        var newAccount = new Account()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PhoneNumber = newPatient.PhoneNumber,
        };

        _repositoryManager.AccountsRepository.Create(newAccount);

        var newPatientEntity = new Patient()
        {
            Account = newAccount,
            FirstName = newPatient.FirstName,
            LastName = newPatient.LastName,
            MiddleName = newPatient.MiddleName,
            DateOfBirth = newPatient.DateOfBirth,
        };

        _repositoryManager.PatientsRepository.Create(newPatientEntity);

        await _repositoryManager.SaveAsync();

        var patientResult = _mapper.Map<PatientResponseDTO>(newPatientEntity);

        return patientResult;
    }

    public async Task UpdatePatientAsync(Guid id, PatientUpdateDTO updatedPatient)
    {
        var patientEntity = await _repositoryManager.PatientsRepository.GetByIdAsync(id, true);

        if (patientEntity is null)
        {
            throw new Exception();
        }

        _mapper.Map(updatedPatient, patientEntity);

        await _repositoryManager.SaveAsync();
    }

    public async Task DeletePatientAsync(Guid id)
    {
        var patientEntity = await _repositoryManager.PatientsRepository.GetByIdAsync(id, true);

        _repositoryManager.AccountsRepository.Delete(patientEntity.Account);

        await _repositoryManager.SaveAsync();
    }
}
