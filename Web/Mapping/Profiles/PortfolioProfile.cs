﻿using AutoMapper;
using Core.Domain.Portfolios;
using Web.ViewModels.PortfolioViewModels;

namespace Web.Mapping.Profiles
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioDetailsViewModel>()
                .ForMember(dest => dest.PortfolioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PortfolioName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PortfolioNumber, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.PortfolioAccountNumber, opt => opt.MapFrom(src => src.Account.Number))
                .ForMember(dest => dest.PortfolioAccountOwner, opt => opt.MapFrom(src => src.Account.Partner.Name))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Id))
                .ReverseMap();

            CreateMap<Portfolio, NewPortfolioViewModel>()
                .ForMember(dest => dest.SelectedAccountId, opt => opt.MapFrom(src => src.AccountId))
                .ReverseMap();
        }
    }
}
