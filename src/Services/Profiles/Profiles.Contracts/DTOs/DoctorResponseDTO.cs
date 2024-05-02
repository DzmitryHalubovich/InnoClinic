namespace Profiles.Contracts.DTOs;

public record DoctorResponseDTO(Guid DoctorId, string FirstName, string LastName, string? MiddleName,
    DateTime DateOfBirth, DateTime CareerStartYear, string Status);

