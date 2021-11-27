using LocadoraVeiculos.Dominio.VeiculoModule;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.VeiculoModule
{
    public interface IVeiculoAppService
    {
        List<Veiculo> SelecionarTodos(bool carregarLocacoes = true);
    }

    public class VeiculoAppService : IVeiculoAppService
    {
        private readonly IVeiculoRepository veiculoRepository;

        public VeiculoAppService(IVeiculoRepository veiculoRepository)
        {
            this.veiculoRepository = veiculoRepository;
        }

        public List<Veiculo> SelecionarTodos(bool carregarLocacoes = true)
        {
            return veiculoRepository.SelecionarTodos(carregarLocacoes: true);
        }
    }
}
