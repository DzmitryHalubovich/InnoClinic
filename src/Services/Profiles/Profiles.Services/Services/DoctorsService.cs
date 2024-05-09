using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Contracts.DTOs.OuterServicesModels;
using Profiles.Contracts.Pagination;
using Profiles.Domain.Entities;
using Profiles.Domain.Exceptions;
using Profiles.Domain.Interfaces;
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

        try
        {
            var office = await _httpClient.GetFromJsonAsync<OfficeDTO>($"{newDoctor.OfficeId}");

            doctorResult.Office = office;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong during the request to OfficeAPI");
        }
            
        return doctorResult;
    }

    public async Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(DoctorsQueryParameters queryParameters, bool trackChanges)
    {
        var doctors = await _repositoryManager.DoctorsRepository
            .GetAllAsync(queryParameters, trackChanges);

        if (doctors.IsNullOrEmpty())
        {
            throw new NotFoundException("There are not doctors in the database.");
        }

        var mappedDoctors = _mapper.Map<List<DoctorResponseDTO>>(doctors);

        try
        {
            IEnumerable<string> officesIds = doctors.Select(x => x.OfficeId).Distinct().ToList();

            string stringWithOfficesIds = string.Join(',', officesIds);

            var offices = await _httpClient.GetFromJsonAsync<List<OfficeDTO>>($"collection/({stringWithOfficesIds})");

            foreach (var doctor in mappedDoctors)
            {
                doctor.Office = offices.First(x => x.OfficeId.Equals(doctor.Office.OfficeId));
            }
        }
        catch(Exception)
        {
            throw new Exception("Something went wrong during the request to OfficeAPI");
        }

        return mappedDoctors;
    }

    public async Task<DoctorResponseDTO?> GetDoctorByIdAsync(Guid id, bool trackChanges)
    {
        var doctor = await _repositoryManager.DoctorsRepository.GetByIdAsync(id, trackChanges);

        if (doctor is null)
        {
            throw new NotFoundException($"Doctor with id: {id} was not found.");
        }

        var mappedDoctor = _mapper.Map<DoctorResponseDTO>(doctor);

        try
        {
            var office = await _httpClient.GetFromJsonAsync<OfficeDTO>($"{doctor.OfficeId}");

            mappedDoctor.Office = office;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong during the request to OfficeAPI");
        }

        return mappedDoctor;
    }

    public async Task UpdateDoctorAsync(Guid id, DoctorUpdateDTO updatedDoctor)
    {
        var doctorEntity = await _repositoryManager.DoctorsRepository.GetByIdAsync(id, true);

        if (doctorEntity is null)
        {
            throw new NotFoundException($"The doctor with id: {id} was not found in the database.");
        }

        _mapper.Map(updatedDoctor, doctorEntity);

        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteDoctorAsync(Guid id)
    {
        var doctorEntity = await _repositoryManager.DoctorsRepository.GetByIdAsync(id, true);

        _repositoryManager.AccountsRepository.Delete(doctorEntity.Account);

        await _repositoryManager.SaveAsync();
    }
}
