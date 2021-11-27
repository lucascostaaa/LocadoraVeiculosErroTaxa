using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM.GrupoVeiculoModule
{
    public class GrupoVeiculoOrmDao : RepositoryOrmBase<GrupoVeiculo, int>,
        IGrupoVeiculoRepository
    {
        public GrupoVeiculoOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false)
        {
            List<GrupoVeiculo> gruposComPlanos = null;

            if (carregarPlanos)
                gruposComPlanos = dbSet
                    .Include(x => x.PlanosCobranca)
                    .ToList();
            else
                gruposComPlanos = dbSet.ToList();

            return gruposComPlanos;
        }
    }
}
