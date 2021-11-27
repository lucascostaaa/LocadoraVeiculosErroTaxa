using LocadoraVeiculos.Dominio.VeiculoModule;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM.VeiculoModule
{
    public class VeiculoOrmDao : RepositoryOrmBase<Veiculo, int>, IVeiculoRepository
    {
        public VeiculoOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public List<Veiculo> SelecionarTodos(bool carregarLocacoes = false)
        {
            if (carregarLocacoes)
                return db.Veiculos.Include(x => x.Locacoes).ToList();

            return db.Veiculos.ToList();
        }
    }
}
