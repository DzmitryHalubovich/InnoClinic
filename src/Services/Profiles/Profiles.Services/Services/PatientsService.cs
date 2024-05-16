using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using OneOf.Types;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Contracts.Pagination;
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

    public async Task<OneOf<List<PatientResponseDTO>, NotFound>> GetAllPatientsAsync(PatientsQueryParameters queryParameters, bool trackCahanges)
    {
        var patients = await _repositoryManager.PatientsRepository.GetAllAsync(queryParameters, trackCahanges);

        if (patients.IsNullOrEmpty())
        {
            return new NotFound();
        }

        var mappedPatients = _mapper.Map<List<PatientResponseDTO>>(patients);

        return mappedPatients;
    }

    public async Task<OneOf<PatientResponseDTO, NotFound>> GetPatientByIdAsync(Guid id, bool trackChanges)
    {
        var patient = await _repositoryManager.PatientsRepository.GetByIdAsync(id, trackChanges);

        if (patient is null)
        {
            return new NotFound();
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

    public async Task<OneOf<Success, NotFound>> UpdatePatientAsync(Guid id, PatientUpdateDTO updatedPatient)
    {
        var patientEntity = await _repositoryManager.PatientsRepository.GetByIdAsync(id, true);

        if (patientEntity is null)
        {
            return new NotFound();
        }

        _mapper.Map(updatedPatient, patientEntity);

        await _repositoryManager.SaveAsync();

        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> DeletePatientAsync(Guid id)
    {
        var patientEntity = await _repositoryManager.PatientsRepository.GetByIdAsync(id, true);

        if (patientEntity is null)
        {
            return new NotFound();
        }

        _repositoryManager.AccountsRepository.Delete(patientEntity.Account);

        await _repositoryManager.SaveAsync();

        return new Success();
    }
}
