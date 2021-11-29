using LocadoraVeiculos.Dominio.Shared;
using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.GrupoVeiculoModule
{
    public interface IGrupoVeiculoRepository : IRepository<GrupoVeiculo, int>, IDisposable
    {
        GrupoVeiculo SelecionarPorId(int id);

        List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false);

        bool VerificarNomeExistente(string nome);
    }
}
