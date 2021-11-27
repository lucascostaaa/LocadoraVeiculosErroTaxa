using AutoMapper;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.WebApplication.AutoMapperConfig
{
    public class CupomProfile : Profile
    {
        public CupomProfile()
        {
            ConfigurarConversaoDeDominioParaViewModel();

            ConfigurarConversaoDeViewModelParaDominio();
        }

        private void ConfigurarConversaoDeDominioParaViewModel()
        {
            CreateMap<List<Cupom>, CupomIndexViewModel>()
                .ForMember(dest => dest.Registros, opt => opt.MapFrom(src => src.ToList()));

            CreateMap<Cupom, CupomListViewModel>();

            CreateMap<Cupom, CupomEditViewModel>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => Convert.ToInt32(src.Tipo)));

            CreateMap<Cupom, CupomDetailsViewModel>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.GetDescription() ));

            CreateMap<Cupom, CupomDeleteViewModel>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.GetDescription()));
        }

        private void ConfigurarConversaoDeViewModelParaDominio()
        {
            CreateMap<CupomCreateViewModel, Cupom>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoCupomEnum)src.Tipo));

            CreateMap<CupomEditViewModel, Cupom>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoCupomEnum)src.Tipo));
        }
    }
}
