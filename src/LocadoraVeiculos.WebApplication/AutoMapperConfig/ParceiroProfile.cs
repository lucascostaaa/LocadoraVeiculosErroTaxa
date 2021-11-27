using AutoMapper;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.WebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LocadoraVeiculos.WebApplication.AutoMapperConfig
{
    public class ParceiroProfile : Profile
    {
        public ParceiroProfile()
        {
            ConfigurarConversaoDeDominioParaViewModel();

            ConfigurarConversaoDeViewModelParaDominio();
        }

        private void ConfigurarConversaoDeDominioParaViewModel()
        {
            CreateMap<List<Parceiro>, ParceiroIndexViewModel>()
                .ForMember(dest => dest.Registros, opt => opt.MapFrom(a => a));

            CreateMap<Parceiro, ParceiroEditViewModel>();

            CreateMap<Parceiro, ParceiroDeleteViewModel>();

            CreateMap<Parceiro, ParceiroDetailsViewModel>();

            CreateMap<Parceiro, ParceiroListViewModel>();

            CreateMap<Parceiro, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
        }

        private void ConfigurarConversaoDeViewModelParaDominio()
        {
            CreateMap<ParceiroCreateViewModel, Parceiro>();

            CreateMap<ParceiroEditViewModel, Parceiro>();
        }
    }
}
