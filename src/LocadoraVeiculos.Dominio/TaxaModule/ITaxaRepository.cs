using LocadoraVeiculos.Dominio.Shared;
using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.TaxaModule
{
    public interface ITaxaRepository : IRepository<Taxa, int>, IReadOnlyRepository<Taxa, int>, IDisposable
    {
        List<Taxa> SelecionarTaxasNaoAdicionadas(List<Taxa> taxasJaAdicionadas);

        bool VerificarTaxaExistente(string nome);
    }
}
