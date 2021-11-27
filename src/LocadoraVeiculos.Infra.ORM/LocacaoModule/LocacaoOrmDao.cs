using LocadoraVeiculos.Dominio.LocacaoModule;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM.LocacaoModule
{
    public class LocacaoOrmDao : RepositoryOrmBase<Locacao, int>, ILocacaoRepository
    {
        public LocacaoOrmDao(LocadoraDbContext db) : base(db)
        {
        }

        public Locacao SelecionarPorId(int id, bool carregarLocacaoCompleta = true)
        {
            if (carregarLocacaoCompleta)
                return db.Locacoes
                    .Include(x => x.TaxasSelecionadas)
                    .Include(x => x.Funcionario)
                    .Include(x => x.Veiculo)
                    .ThenInclude(x => x.GrupoVeiculo)
                    .Include(x => x.Condutor)
                    .ThenInclude(c => c.Cliente)
                    .FirstOrDefault(x => x.Id == id);

            return db.Locacoes.FirstOrDefault(x => x.Id == id);
        }

        public List<Locacao> SelecionarTodos(bool carregarTaxas = false)
        {
            if (carregarTaxas)
                return db.Locacoes
                    .Include(x => x.TaxasSelecionadas)
                    .Include(x => x.Veiculo)
                    .Include(x => x.Condutor)
                    .ThenInclude(c => c.Cliente)
                    .ToList();

            return db.Locacoes
                .Include(x => x.Veiculo)
                .Include(x => x.Condutor)
                .ThenInclude(c => c.Cliente)
                .ToList();
        }

        public override bool Excluir(Locacao entity)
        {
            return base.Excluir(entity);
        }

        public override bool Inserir(Locacao entity)
        {
            if (db.Entry(entity.Funcionario).State != EntityState.Unchanged)
                db.Attach(entity.Funcionario);

            return base.Inserir(entity);
        }

        //https://stackoverflow.com/questions/36856073/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-this-t
        //https://www.thereformedprogrammer.net/updating-many-to-many-relationships-in-ef-core-5-and-above/
        public override bool Editar(int id, Locacao locacaoAlterada)
        {
            try
            {
                var local = db.Set<Locacao>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(id));

                if (local != null)
                    db.Entry(local).State = EntityState.Detached;

                locacaoAlterada.Id = id;
                db.Entry(locacaoAlterada).State = EntityState.Modified;

                var taxasRemovidas = locacaoAlterada.TaxasRemovidas();

                foreach (var taxa in taxasRemovidas)
                    locacaoAlterada.RemoverTaxaLocacao(taxa);

                db.SaveChanges();
            }
            catch (System.Exception ex)
            {

            }

            return true;
        }
    }
}