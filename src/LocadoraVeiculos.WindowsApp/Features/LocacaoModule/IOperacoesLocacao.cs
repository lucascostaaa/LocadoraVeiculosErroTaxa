using LocacaoVeiculos.WindowsApp.Shared;

namespace LocadoraVeiculos.WindowsApp.Features.LocacaoModule
{
    public interface IOperacoesLocacao : ICadastravel
    {
        void ConcluirOperacao();
    }
}
