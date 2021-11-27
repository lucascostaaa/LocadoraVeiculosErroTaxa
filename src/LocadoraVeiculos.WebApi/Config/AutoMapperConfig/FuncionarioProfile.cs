using AutoMapper;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.Config.AutoMapperConfig
{
    public class FuncionarioProfile : Profile
    {
        public FuncionarioProfile()
        {
            CreateMap<Funcionario, FuncionarioListViewModel>();

            CreateMap<Funcionario, FuncionarioDetailsViewModel>();

            CreateMap<FuncionarioCreateViewModel, Funcionario>();

            CreateMap<FuncionarioEditViewModel, Funcionario>();
        }
    }
}
