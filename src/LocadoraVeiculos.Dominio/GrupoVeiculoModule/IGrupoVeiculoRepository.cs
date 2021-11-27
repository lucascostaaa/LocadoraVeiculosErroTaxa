using LocadoraVeiculos.Dominio.Shared;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.GrupoVeiculoModule
{
    public interface IGrupoVeiculoRepository : IRepository<GrupoVeiculo, int>
    {
        GrupoVeiculo SelecionarPorId(int id);

        List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false);
    }
}
