using LocadoraVeiculos.Dominio.TaxaModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM.TaxaModule
{
    public class TaxaOrmDao : RepositoryOrmBase<Taxa, int>, ITaxaRepository
    {
        public TaxaOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<Taxa> SelecionarTaxasNaoAdicionadas(List<Taxa> taxasJaAdicionadas)
        {
            throw new NotImplementedException();
        }

        public bool VerificarTaxaExistente(string nome)
        {
            return db.Taxas.Count(x => x.Nome == nome) > 0;
        }

    }
}
