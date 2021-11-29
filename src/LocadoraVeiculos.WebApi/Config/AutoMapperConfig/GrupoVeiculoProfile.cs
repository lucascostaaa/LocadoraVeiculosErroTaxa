using AutoMapper;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.Config.AutoMapperConfig
{
    public class GrupoVeiculoProfile : Profile
    {
        public GrupoVeiculoProfile()
        {
            CreateMap<GrupoVeiculo, GrupoVeiculoListViewModel>();

            CreateMap<GrupoVeiculo, GrupoVeiculoDetailsViewModel>();

            CreateMap<GrupoVeiculoCreateViewModel, GrupoVeiculo>();

            CreateMap<GrupoVeiculoEditViewModel, GrupoVeiculo>();
        }
    }
}
