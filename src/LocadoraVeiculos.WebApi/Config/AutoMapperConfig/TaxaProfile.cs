using AutoMapper;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.Config.AutoMapperConfig
{
    public class TaxaProfile: Profile
    {
        public TaxaProfile()
        {
            CreateMap<Taxa, TaxaListViewModel>()
            .ForMember(dest => dest.TipoTaxa, opt => opt.MapFrom(src => src.TipoTaxa))
            .ForMember(dest => dest.EstadoTaxaLocacao, opt => opt.MapFrom(src => (int)src.EstadoTaxaLocacao));

            CreateMap<Taxa, TaxaDetailsViewModel>()
            .ForMember(dest => dest.TipoTaxa, opt => opt.MapFrom(src => (int)src.TipoTaxa))
            .ForMember(dest => dest.EstadoTaxaLocacao, opt => opt.MapFrom(src => (int)src.EstadoTaxaLocacao));

            CreateMap<TaxaCreateViewModel, Taxa>()
            .ForMember(dest => dest.TipoTaxa, opt => opt.MapFrom(src => (TipoTaxaEnum)src.TipoTaxa))
            .ForMember(dest => dest.EstadoTaxaLocacao, opt => opt.MapFrom(src => (EstadoTaxaLocacaoEnum)src.EstadoTaxaLocacao));

            CreateMap<TaxaEditViewModel, Taxa>()
              .ForMember(dest => dest.TipoTaxa, opt => opt.MapFrom(src => (TipoTaxaEnum)src.TipoTaxa))
              .ForMember(dest => dest.EstadoTaxaLocacao, opt => opt.MapFrom(src => (EstadoTaxaLocacaoEnum)src.EstadoTaxaLocacao));
        }


    }
}
