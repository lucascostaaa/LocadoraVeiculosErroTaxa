using LocadoraVeiculos.Dominio.Shared;
using System;

namespace LocadoraVeiculos.Dominio.CupomModule
{
    public interface IParceiroRepository : IRepository<Parceiro, int>, IReadOnlyRepository<Parceiro, int>, IDisposable
    {
        Parceiro SelecionarPorId(int id, bool carregarCupons = true);

        bool VerificarNomeExistente(string nome);
    }
}   