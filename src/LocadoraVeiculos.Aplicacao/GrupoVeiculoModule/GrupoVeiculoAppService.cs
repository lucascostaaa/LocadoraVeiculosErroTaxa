using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.GrupoVeiculoModule
{
    public interface IGrupoVeiculoAppService
    {
        List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false);
    }

    public class GrupoVeiculoAppService : IGrupoVeiculoAppService
    {
        private readonly IGrupoVeiculoRepository grupoVeiculoRepository;

        public GrupoVeiculoAppService(IGrupoVeiculoRepository grupoVeiculoRepository)
        {
            this.grupoVeiculoRepository = grupoVeiculoRepository;
        }

        public List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false)
        {
            return grupoVeiculoRepository.SelecionarTodos(carregarPlanos);
        }
    }
}
