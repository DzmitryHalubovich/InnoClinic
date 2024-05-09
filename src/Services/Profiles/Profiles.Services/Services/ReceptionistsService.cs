using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Profiles.Contracts.DTOs.OuterServicesModels;
using Profiles.Contracts.DTOs.Receptionist;
using Profiles.Domain.Entities;
using Profiles.Domain.Exceptions;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;
using System.Net.Http.Json;

namespace Profiles.Services.Services;

public class ReceptionistsService : IReceptionistsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public ReceptionistsService(IRepositoryManager repositoryManager, IMapper mapper, IHttpClientFactory factory)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7255/api/offices/");
    }

    public async Task<List<ReceptionistResponseDTO>> GetAllReceptionistsAsync(bool trackChanges)
    {
        var receptionists = await _repositoryManager.ReceptionistsRepository.GetAllAsync(trackChanges);

        if (receptionists.IsNullOrEmpty())
        {
            throw new NotFoundException("There are not receptionists in the database.");
        }

        var mappedReceptionists = _mapper.Map<List<ReceptionistResponseDTO>>(receptionists);

        try
        {
            IEnumerable<string> officesIds = receptionists.Select(x => x.OfficeId).Distinct().ToList();

            string stringWithOfficesIds = string.Join(',', officesIds);

            var offices = await _httpClient.GetFromJsonAsync<List<OfficeDTO>>($"collection/({stringWithOfficesIds})");

            foreach (var receptionist in mappedReceptionists)
            {
                receptionist.Office = offices.First(x => x.OfficeId.Equals(receptionist.Office.OfficeId));
            }
        }
        catch (Exception) 
        {
            throw new Exception("Something went wrong during the request to OfficeAPI");
        }

        return mappedReceptionists;
    }

    public async Task<ReceptionistResponseDTO?> GetReceptionistByIdAsync(Guid id, bool trackChanges)
    {
        var receptionist = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(id, trackChanges);

        if (receptionist is null)
        {
            throw new NotFoundException($"Receptionist with id: {id} was not found in the database.");
        }

        var mappedReceptionist = _mapper.Map<ReceptionistResponseDTO>(receptionist);

        try
        {
            var office = await _httpClient.GetFromJsonAsync<OfficeDTO>($"{mappedReceptionist.Office.OfficeId}");

            mappedReceptionist.Office = office;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong during the request to OfficeAPI");
        }

        return mappedReceptionist;
    }

    public async Task<ReceptionistResponseDTO> CreateReceptionistAsync(ReceptionistCreateDTO newReceptionist)
    {
        var newAccount = new Account()
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Email = newReceptionist.Email,
        };

        _repositoryManager.AccountsRepository.Create(newAccount);

        var newReceptionistEntity = new Receptionist()
        {
            Account = newAccount,
            FirstName = newReceptionist.FirstName,
            LastName = newReceptionist.LastName,
            MiddleName = newReceptionist.MiddleName,
            OfficeId = newReceptionist.OfficeId,
        };

        _repositoryManager.ReceptionistsRepository.Create(newReceptionistEntity);

        await _repositoryManager.SaveAsync();
        
        var receptionistResult = _mapper.Map<ReceptionistResponseDTO>(newReceptionistEntity);
        
        try
        {
            var office = await _httpClient.GetFromJsonAsync<OfficeDTO>($"{newReceptionist.OfficeId}");

            receptionistResult.Office = office;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong during the request to OfficeAPI");
        }

        return receptionistResult;
    }

    public async Task UpdateReceptionistAsync(Guid id, ReceptionistUpdateDTO updatedReceptionist)
    {
        var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(id, true);

        if (receptionistEntity is null)
        {
            throw new NotFoundException($"The receptionist with id: {id} was not found in the database.");
        }

        _mapper.Map(updatedReceptionist, receptionistEntity);

        receptionistEntity.OfficeId = updatedReceptionist.OfficeId;

        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteReceptionistAsync(Guid id)
    {
        var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(id, true);

        _repositoryManager.AccountsRepository.Delete(receptionistEntity.Account);

        await _repositoryManager.SaveAsync();
    }
}
