using LocadoraVeiculos.Dominio.Shared;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.ClienteModule
{
    public interface ICondutorRepository : IRepository<Condutor, int>
    {
        Condutor SelecionarPorId(int id);

        List<Condutor> SelecionarTodos(bool carregarClientes = false);
    }
}
