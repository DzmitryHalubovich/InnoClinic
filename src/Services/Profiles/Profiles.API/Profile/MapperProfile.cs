using AutoMapper;
using Profiles.Contracts.DTOs;
using Profiles.Domain.Entities;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Doctor, DoctorResponseDTO>()
            .ForMember(dest => dest.FullName,
            opt => opt.MapFrom(src => string.Join(' ', 
            new[] { src.LastName, src.FirstName, src.MiddleName }
                .Where(s => !string.IsNullOrEmpty(s)))))
            .ForMember(dest => dest.OfficeAddress, 
            opt => opt.MapFrom(src => src.Office.Address))
            .ForMember(dest => dest.Specialization, 
            opt => opt.MapFrom(src => src.Specialization.SpecializationName))
            .ForMember(dest => dest.Experience, 
            opt => opt.MapFrom(src => DateTime.Now.AddYears(1).AddYears(-src.CareerStartYear.Year).Year));
    }
}
