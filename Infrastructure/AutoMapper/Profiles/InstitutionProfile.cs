﻿using AutoMapper;
using Core.Domain.Institution;
using Core.Dtos;

namespace Infrastructure.AutoMapper.Profiles
{
    public class InstitutionProfile : Profile
    {
        public InstitutionProfile()
        {
            CreateMap<BaseInstitution, InstitutionDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Bank, InstitutionDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Bic, opt => opt.MapFrom(src => src.Bic))
                .ReverseMap();
        }
    }
}
