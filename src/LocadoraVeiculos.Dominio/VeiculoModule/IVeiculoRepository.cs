using LocadoraVeiculos.Dominio.Shared;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.VeiculoModule
{
    public interface IVeiculoRepository :
        IRepository<Veiculo, int>
    {
        List<Veiculo> SelecionarTodos(bool carregarLocacoes = false);

        Veiculo SelecionarPorId(int id);
    }
}
