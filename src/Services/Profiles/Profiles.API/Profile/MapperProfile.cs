using AutoMapper;
using Profiles.Contracts.DTOs.Doctor;
using Profiles.Contracts.DTOs.Patient;
using Profiles.Contracts.DTOs.PersonalInfo;
using Profiles.Domain.Entities;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Doctor, DoctorResponseDTO>()
            .ForMember(dest => dest.FullName,
            opt => opt.MapFrom(src => string.Join(' ', 
            new[] { src.Account.PersonalInfo.LastName, src.Account.PersonalInfo.FirstName, src.Account.PersonalInfo.MiddleName }
                .Where(s => !string.IsNullOrEmpty(s)))))
            .ForMember(dest => dest.OfficeAddress, 
            opt => opt.MapFrom(src => src.Office.Address))
            .ForMember(dest => dest.Specialization, 
            opt => opt.MapFrom(src => src.Specialization.SpecializationName))
            .ForMember(dest => dest.Experience, 
            opt => opt.MapFrom(src => DateTime.Now.AddYears(1).AddYears(-src.CareerStartYear.Year).Year));

        CreateMap<DoctorCreateDTO, Doctor>();

        CreateMap<PersonalInfoCreateDTO, PersonalInfo>();

        CreateMap<PatientCreateDTO, Patient>();
        CreateMap<Patient, PatientResponseDTO>()
            .ForPath(dest => dest.PersonalInfo.FirstName,
            opt => opt.MapFrom(src => src.Account.PersonalInfo.FirstName))
            .ForPath(dest => dest.PersonalInfo.LastName,
                opt => opt.MapFrom(src => src.Account.PersonalInfo.LastName))
            .ForPath(dest => dest.PersonalInfo.MiddleName,
                opt => opt.MapFrom(src => src.Account.PersonalInfo.MiddleName))
            .ForPath(dest => dest.PersonalInfo.PhoneNumber,
                opt => opt.MapFrom(src => src.Account.PersonalInfo.PhoneNumber))
            .ForPath(dest => dest.PersonalInfo.DateOfBirth,
                opt => opt.MapFrom(src => src.Account.PersonalInfo.DateOfBirth))
            .ForPath(dest => dest.PersonalInfo.PhoneNumber,
            opt => opt.MapFrom(src => src.Account.PersonalInfo.PhoneNumber));
    }
}
