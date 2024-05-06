using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Profiles.Contracts.DTOs;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.OuterServicesModels;
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
        var newPersonalInfo = _mapper.Map<PersonalInformation>(newDoctor.PersonalInfo);

        var createdPersonalInfo = await _repositoryManager.PersonalInfoRepository
            .AddPersonalInfoAsync(newPersonalInfo);

        var newAccount = new Account() 
        {
            Email = newDoctor.Email,
            CreatedAt = DateTime.UtcNow,
            PersonalInformationId = createdPersonalInfo.PersonalInformationId,
            UpdatedAt = DateTime.UtcNow
        };

        var createdAccount = await _repositoryManager.AccountRepository.CreateAsync(newAccount);

        var doctorForCreation = _mapper.Map<Doctor>(newDoctor);

        doctorForCreation.AccountId = createdAccount.AccountId;

        var createdDoctor = await _repositoryManager.DoctorsRepository.CreateAsync(doctorForCreation);

        var doctorResponse = _mapper.Map<DoctorResponseDTO>(createdDoctor);

        return doctorResponse;
    }

    public async Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(DoctorsQueryParameters parameters,bool trackChanges)
    {
        var doctors = await _repositoryManager.DoctorsRepository
            .GetAllAsync(parameters.SpecializationId, trackChanges);

        if (doctors.IsNullOrEmpty())
        {
            throw new Exception();
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

        doctor.Office = await _httpClient.GetFromJsonAsync<Office>($"{doctor.OfficeId}");

        var mappedDoctor = _mapper.Map<DoctorResponseDTO>(doctor);

        return mappedDoctor;
    }
}
