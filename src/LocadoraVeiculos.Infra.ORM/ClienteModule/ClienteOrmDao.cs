using LocadoraVeiculos.Dominio.ClienteModule;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM.ClienteModule
{
    public class ClienteOrmDao : RepositoryOrmBase<Cliente, int>,
        IClienteRepository
    {
        public ClienteOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool ExisteClienteComEsteCpf(string cpf)
        {
            return dbSet.FirstOrDefault(x => x.CPF == cpf) != null;
        }

        public List<Cliente> SelecionarTodos(bool carregarCondutores)
        {
            if (carregarCondutores)
                return dbSet
                    .Include(x => x.Condutores)
                    .ToList();

            return dbSet.ToList();
        }
    }
}
