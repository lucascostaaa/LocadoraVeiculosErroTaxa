using LocadoraVeiculos.Dominio.CupomModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM.CupomModule
{
    public class CupomOrmDao : RepositoryOrmBase<Cupom, int>, ICupomRepository
    {
        public CupomOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<Cupom> SelecionarCuponsAtivos(DateTime dataAtual)
        {
            return db.Cupons.Where(x => x.DataValidade < dataAtual).ToList();
        }

        public Cupom SelecionarPorId(int id, bool carregarParceiro = false)
        {

            if (carregarParceiro)
                return db.Cupons
                    .Include(x => x.Parceiro)
                    .FirstOrDefault(x => x.Id == id);

            return db.Cupons
                    .FirstOrDefault(x => x.Id == id);
        }

        public override List<Cupom> SelecionarTodos()
        {
            return db.Cupons
                   .Include(x => x.Parceiro)
                   .ToList();
        }

        public bool VerificarCupomExistente(string nome)
        {
            return db.Cupons.Count(x => x.Nome == nome) > 0;
        }
    }
}
