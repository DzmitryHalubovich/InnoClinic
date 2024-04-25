﻿using AutoMapper;
using Offices.Contracts.DTOs;
using Offices.Domain.Entities;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Office, OfficeResponseDTO>();
        CreateMap<OfficeCreateDTO, Office>();
        CreateMap<OfficeUpdateDTO, Office>();
    }
}
