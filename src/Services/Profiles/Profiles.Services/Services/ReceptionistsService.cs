using AutoMapper;
using Profiles.Contracts.DTOs.Receptionist;
using Profiles.Domain.Entities;
using Profiles.Domain.Exceptions;
using Profiles.Domain.Interfaces;
using Profiles.Services.Abstractions;

namespace Profiles.Services.Services;

public class ReceptionistsService : IReceptionistsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public ReceptionistsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<List<ReceptionistResponseDTO>> GetAllReceptionistsAsync(bool trackChanges)
    {
        var receptionists = await _repositoryManager.ReceptionistsRepository.GetAllAsync(trackChanges);

        var mappedReceptionists = _mapper.Map<List<ReceptionistResponseDTO>>(receptionists);

        return mappedReceptionists;
    }

    public async Task<ReceptionistResponseDTO> GetReceptionistByIdAsync(Guid receptionistId, bool trackChanges)
    {
        var receptionist = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(receptionistId, trackChanges);

        if (receptionist is null)
        {
            throw new NotFoundException($"Receptionist with id: {receptionistId} was not found in the database.");
        }

        var mappedReceptionist = _mapper.Map<ReceptionistResponseDTO>(receptionist);

        return mappedReceptionist;
    }

    public async Task<ReceptionistResponseDTO> CreateReceptionistAsync(ReceptionistCreateDTO newReceptionist)
    {
        var newPersonalInfo = _mapper.Map<PersonalInfo>(newReceptionist.PersonalInfo);

        var newAccount = new Account()
        {
            PersonalInfo = newPersonalInfo,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Email = newReceptionist.Email,
        };

        _repositoryManager.AccountsRepository.Create(newAccount);

        var newReceptionistEntity = new Receptionist()
        {
            Account = newAccount,
            OfficeId = newReceptionist.OfficeId,
        };

        _repositoryManager.ReceptionistsRepository.Create(newReceptionistEntity);

        var receptionistResult = _mapper.Map<ReceptionistResponseDTO>(newReceptionistEntity);

        await _repositoryManager.SaveAsync();

        return receptionistResult;
    }

    public async Task UpdateReceptionistAsync(Guid receptionistId, ReceptionistUpdateDTO updatedReceptionist)
    {
        var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(receptionistId, true);

        if (receptionistEntity is null)
        {
            throw new NotFoundException($"The receptionist with id: {receptionistId} was not found in the database.");
        }

        _mapper.Map(updatedReceptionist.PersonalInfo, receptionistEntity.Account.PersonalInfo);

        receptionistEntity.OfficeId = updatedReceptionist.OfficeId;

        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteReceptionistAsync(Guid receptionistId)
    {
        var receptionistEntity = await _repositoryManager.ReceptionistsRepository.GetByIdAsync(receptionistId, true);

        if (receptionistEntity is null)
        {
            throw new NotFoundException($"The receptionist with id: {receptionistId} was not found in the database.");
        }

        _repositoryManager.ReceptionistsRepository.Delete(receptionistEntity);
        await _repositoryManager.SaveAsync();
    }
}
