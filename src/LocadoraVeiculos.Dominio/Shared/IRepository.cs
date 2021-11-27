namespace LocadoraVeiculos.Dominio.Shared
{
    public interface IRepository<TEntity, TKey>
    {
        bool Inserir(TEntity entity);

        bool Editar(TKey id, TEntity entity);

        bool Editar(TEntity entity);

        bool Excluir(TKey id);

        bool Excluir(TEntity entity);
    }
}
