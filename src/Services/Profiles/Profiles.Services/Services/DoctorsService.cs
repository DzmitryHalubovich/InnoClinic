using AutoMapper;
using Profiles.Contracts.DTOs;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;
using System.Net.Http.Json;

namespace Profiles.Services.Services;

public class DoctorsService : IDoctorsService
{
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public DoctorsService(IDoctorsRepository doctorsRepository, IMapper mapper, IHttpClientFactory factory)
    {
        _doctorsRepository = doctorsRepository;
        _mapper = mapper;
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7255/api/offices/");
    }

    public async Task<List<DoctorResponseDTO>> GetAllDoctorsAsync()
    {
        var doctors = _doctorsRepository.GetAll();

        foreach (var doctor in doctors)
        {
            var office = await _httpClient.GetFromJsonAsync<Office>($"{doctor.OfficeId}");
            doctor.Office = office;
        }

        var mappedDoctors = _mapper.Map<List<DoctorResponseDTO>>(doctors);

        return mappedDoctors;
    }

    public async Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid doctorId)
    {
        var doctor = _doctorsRepository.GetById(doctorId);

        doctor.Office = await _httpClient.GetFromJsonAsync<Office>($"{doctor.OfficeId}");

        var mappedDoctor = _mapper.Map<DoctorResponseDTO>(doctor);

        return mappedDoctor;
    }
}
