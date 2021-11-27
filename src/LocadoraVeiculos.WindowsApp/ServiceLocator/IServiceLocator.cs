namespace LocadoraVeiculos.WindowsApp.ServiceLocator
{
    public interface IServiceLocator
    {
        T Get<T>();
    }
}
