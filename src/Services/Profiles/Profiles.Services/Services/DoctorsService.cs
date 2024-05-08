using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.OuterServicesModels;
using Profiles.Domain.Exceptions;
using Profiles.Domain.Interfaces;
using Profiles.Presentation.Pagination;
using Profiles.Services.Abstractions;
using System.Net.Http.Json;

namespace Profiles.Services.Services;

public class DoctorsService : IDoctorsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public DoctorsService(IRepositoryManager repositoryManager,
        IMapper mapper, IHttpClientFactory factory)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7255/api/offices/");
    }

    public async Task<DoctorResponseDTO> CreateDoctorAsync(DoctorCreateDTO newDoctor)
    {
        var newPersonalInfo = _mapper.Map<PersonalInfo>(newDoctor.PersonalInfo);

        var newAccount = new Account()
        {
            PersonalInfo = newPersonalInfo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Email = newDoctor.Email
        };

        _repositoryManager.AccountsRepository.Create(newAccount);

        var newDoctorEnity = new Doctor()
        {
            Account = newAccount,
            OfficeId = newDoctor.OfficeId,
            CareerStartYear = DateTime.UtcNow,
            Status = newDoctor.Status,
        };
        
        _repositoryManager.DoctorsRepository.Create(newDoctorEnity);

        var doctorResult = _mapper.Map<DoctorResponseDTO>(newDoctorEnity);

        await _repositoryManager.SaveAsync();
        
        return doctorResult;
    }

    public async Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(DoctorsQueryParameters parameters,bool trackChanges)
    {
        var doctors = await _repositoryManager.DoctorsRepository
            .GetAllAsync(parameters.SpecializationId, parameters.SearchLastName, trackChanges);

        if (doctors.IsNullOrEmpty())
        {
            throw new NotFoundException("There are not doctors in the database.");
        }

        IEnumerable<string> officesIds = doctors.Select(x => x.OfficeId).Distinct().ToList();

        string stringWithOfficesIds = string.Join(',', officesIds);

        var offices = await _httpClient.GetFromJsonAsync<List<Office>>($"collection/({stringWithOfficesIds})");

        foreach (var doctor in doctors)
        {
            doctor.Office = offices.FirstOrDefault(x => x.OfficeId.Equals(doctor.OfficeId));
        }

        var mappedDoctors = _mapper.Map<List<DoctorResponseDTO>>(doctors);

        return mappedDoctors;
    }

    public async Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid doctorId, bool trackChanges)
    {
        var doctor = await _repositoryManager.DoctorsRepository.GetByIdAsync(doctorId, trackChanges);

        if (doctor is null)
        {
            throw new NotFoundException($"Doctor with id: {doctorId} was not found.");
        }

        doctor.Office = await _httpClient.GetFromJsonAsync<Office>($"{doctor.OfficeId}");

        var mappedDoctor = _mapper.Map<DoctorResponseDTO>(doctor);

        return mappedDoctor;
    }

    public async Task UpdateDoctorAsync(Guid doctorId, DoctorUpdateDTO updatedDoctor)
    {
        var doctorEntity = await _repositoryManager.DoctorsRepository.GetByIdAsync(doctorId, true);

        if (doctorEntity is null)
        {
            throw new NotFoundException("");
        }

        _mapper.Map(updatedDoctor.PersonalInfo, doctorEntity.Account.PersonalInfo);
        doctorEntity.OfficeId = updatedDoctor.OfficeId;

        await _repositoryManager.SaveAsync();
    }
}
