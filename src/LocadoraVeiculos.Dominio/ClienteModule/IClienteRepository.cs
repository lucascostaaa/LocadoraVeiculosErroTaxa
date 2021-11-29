using LocadoraVeiculos.Dominio.Shared;
using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.ClienteModule
{
    public interface IClienteRepository : IRepository<Cliente, int>, IReadOnlyRepository<Cliente, int>, IDisposable
    {
        bool ExisteClienteComEsteCpf(string cpf);

        Cliente SelecionarPorId(int id);

        List<Cliente> SelecionarTodos(bool carregarCondutores);
    }

}
