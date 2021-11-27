using LocadoraVeiculos.Dominio.Shared;
using System;

namespace LocadoraVeiculos.Dominio.FuncionarioModule
{
    public interface IFuncionarioRepository : IRepository<Funcionario, int>, IReadOnlyRepository<Funcionario, int>, IDisposable
    {
        Funcionario SelecionarFuncionarioLogado();

        bool VerificarUsuarioExistente(string usuario);
    }
}
