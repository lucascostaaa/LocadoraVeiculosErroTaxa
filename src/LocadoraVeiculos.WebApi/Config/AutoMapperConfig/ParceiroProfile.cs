using AutoMapper;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.WebApi.ViewModels;

namespace LocadoraVeiculos.WebApi.AutoMapperConfig.Profiles
{
    public class ParceiroProfile : Profile
    {
        public ParceiroProfile()
        {
            CreateMap<Parceiro, ParceiroListViewModel>();

            CreateMap<Parceiro, ParceiroDetailsViewModel>();

            CreateMap<ParceiroCreateViewModel, Parceiro>();

            CreateMap<ParceiroEditViewModel, Parceiro>();

            CreateMap<Cupom, CupomListViewModel>();
        }
    }
}
