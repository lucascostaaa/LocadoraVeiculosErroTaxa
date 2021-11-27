using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.Shared
{
    public interface IReadOnlyRepository<TEntity, TKey>
    {
        List<TEntity> SelecionarTodos();

        TEntity SelecionarPorId(TKey id);
    }
}
