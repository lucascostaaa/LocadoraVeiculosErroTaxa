using AutoMapper;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApplication.AutoMapperConfig
{
    public class FuncionarioProfile : Profile
    {
        public FuncionarioProfile()
        {
            ConfigurarConversaoDeDominioParaViewModel();

            ConfigurarConversaoDeViewModelParaDominio();
        }

        private void ConfigurarConversaoDeViewModelParaDominio()
        {
         
        }

        private void ConfigurarConversaoDeDominioParaViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
