using AutoMapper;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.WebApi.ViewModels;

namespace LocadoraVeiculos.WebApi.AutoMapperConfig.Profiles
{
    public class CupomProfile : Profile
    {
        public CupomProfile()
        {
            CreateMap<Cupom, CupomListViewModel>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));

            CreateMap<Cupom, CupomDetailsViewModel>()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (int)src.Tipo));

            CreateMap<CupomCreateViewModel, Cupom>()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoCupomEnum)src.Tipo));

            CreateMap<CupomEditViewModel, Cupom>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoCupomEnum)src.Tipo));
        }
    }
}
