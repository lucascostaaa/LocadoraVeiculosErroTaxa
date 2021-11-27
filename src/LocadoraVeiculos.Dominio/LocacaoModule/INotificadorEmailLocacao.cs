namespace LocadoraVeiculos.Dominio.LocacaoModule
{
    public interface INotificadorEmailLocacao
    {
        bool EnviarEmailLocacao(Locacao locacao);
    }
}