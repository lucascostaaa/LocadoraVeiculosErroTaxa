using LocadoraVeiculos.Dominio.Shared;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.LocacaoModule
{
    public interface ILocacaoRepository : IRepository<Locacao, int>
    {
        Locacao SelecionarPorId(int id, bool carregarLocacaoCompleta = true);

        public List<Locacao> SelecionarTodos(bool carregarTaxas = false);
    }
}
