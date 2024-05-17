using AutoMapper;
using Services.Contracts.Specialization;
using Services.Domain.Entities;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<SpecializationCreateDTO, Specialization>();

        CreateMap<Specialization, SpecializationResponseDTO>();

        CreateMap<SpecializationUpdateDTO, Specialization>();
    }
}
