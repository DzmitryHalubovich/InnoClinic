using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using OneOf.Types;
using Profiles.Contracts.DTOs.OuterServicesModels;
using Profiles.Contracts.DTOs.Receptionist;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;

namespace Profiles.Services.Services;

public class ReceptionistsService : IReceptionistsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IHttpRepository<OfficeDTO> _httpRepository;

    public ReceptionistsService(IRepositoryManager repositoryManager, 
        IMapper mapper, IHttpRepository<OfficeDTO> httpRepository)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _httpRepository = httpRepository;
    }

    public async Task<OneOf<List<ReceptionistResponseDTO>, NotFound>> GetAllReceptionistsAsync(bool trackChanges)
    {
        var receptionists = await _repositoryManager.ReceptionistsRepository.GetAllAsync(trackChanges);

        if (receptionists.IsNullOrEmpty())
        {
            return new NotFound();
        }

        var mappedReceptionists = _mapper.Map<List<ReceptionistResponseDTO>>(receptionists);

        IEnumerable<string> officesIds = receptionists.Select(x => x.OfficeId).Distinct().ToList();

        var offices = await _httpRepository.GetCollection(officesIds);

        foreach (var receptionist in mappedReceptionists)
        {
            receptionist.Office = offices.First(x => x.OfficeId.Equals(receptionist.Office.OfficeId));
        }

        return mappedReceptionists;
    }

    public async Task<OneOf<ReceptionistResponseDTO, NotFound>> GetReceptionistByIdAsync(Guid id, bool trackChanges)
    {
        var receptionist = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(id, trackChanges);

        if (receptionist is null)
        {
            return new NotFound();
        }

        var mappedReceptionist = _mapper.Map<ReceptionistResponseDTO>(receptionist);

            mappedReceptionist.Office = 
                await _httpRepository.GetOneAsync("https://localhost:7255/api/offices", receptionist.OfficeId!);

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

        receptionistResult.Office =  
            await _httpRepository.GetOneAsync("https://localhost:7255/api/offices", newReceptionistEntity.OfficeId!);

        return receptionistResult;
    }

    public async Task<OneOf<Success, NotFound>> UpdateReceptionistAsync(Guid id, ReceptionistUpdateDTO updatedReceptionist)
    {
        var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(id, true);

        if (receptionistEntity is null)
        {
            return new NotFound();
        }

        _mapper.Map(updatedReceptionist, receptionistEntity);

        receptionistEntity.OfficeId = updatedReceptionist.OfficeId;

        await _repositoryManager.SaveAsync();

        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> DeleteReceptionistAsync(Guid id)
    {
        var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(id, true);

        if (receptionistEntity is null)
        {
            return new NotFound();
        }

        _repositoryManager.AccountsRepository.Delete(receptionistEntity.Account);

        await _repositoryManager.SaveAsync();

        return new Success();
    }
}
