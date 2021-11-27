using LocadoraVeiculos.Dominio.CupomModule;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM.CupomModule
{
    public class ParceiroOrmDao : RepositoryOrmBase<Parceiro, int>, IParceiroRepository
    {
        public ParceiroOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Parceiro SelecionarPorId(int id, bool carregarCupons = false)
        {
            if (carregarCupons)
                return db.Parceiros
                    .Include(x => x.Cupons)
                    .FirstOrDefault(x => x.Id == id);

            return db.Parceiros
                .FirstOrDefault(x => x.Id == id);
        }

        public bool VerificarNomeExistente(string nome)
        {
            return db.Parceiros.Count(x => x.Nome == nome) > 0;
        }
    }
}
