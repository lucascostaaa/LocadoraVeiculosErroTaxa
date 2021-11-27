using LocadoraVeiculos.Dominio.Shared;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.ClienteModule
{
    public interface IClienteRepository : IRepository<Cliente, int>
    {
        bool ExisteClienteComEsteCpf(string cpf);

        Cliente SelecionarPorId(int id);

        List<Cliente> SelecionarTodos(bool carregarCondutores);
    }

}
