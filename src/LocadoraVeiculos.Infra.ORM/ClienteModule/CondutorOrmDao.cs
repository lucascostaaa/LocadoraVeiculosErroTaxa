using LocadoraVeiculos.Dominio.ClienteModule;
using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Infra.ORM.ClienteModule
{
    public class CondutorOrmDao : RepositoryOrmBase<Condutor, int>, ICondutorRepository
    {
        public CondutorOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public List<Condutor> SelecionarTodos(bool carregarClientes = false)
        {
            throw new NotImplementedException();
        }
    }
}
