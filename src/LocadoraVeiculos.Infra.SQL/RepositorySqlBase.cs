using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.Shared;
using System.Collections.Generic;
using System.Data;

namespace LocadoraVeiculos.Infra.SQL
{
    public abstract class RepositorySqlBase<TEntity, TKey> :
        IReadOnlyRepository<TEntity, TKey>,
        IRepository<TEntity, TKey>
        where TEntity : EntidadeBase<TKey>, new()
    {
        protected abstract string SqlInserir { get; }
        protected abstract string SqlEditar { get; }
        protected abstract string SqlExcluir { get; }
        protected abstract string SqlSelecionarTodos { get; }
        protected abstract string SqlSelecionarPorId { get; }

        protected abstract Dictionary<string, object> ObterParametros(TEntity entity);
        protected abstract TEntity Converter(IDataReader reader);


        public virtual bool Inserir(TEntity entity)
        {
            return Db.Insert(SqlInserir, ObterParametros(entity)) > 0;
        }

        public virtual bool Editar(TKey id, TEntity entity)
        {
            Db.Update(SqlEditar, ObterParametros(entity));

            return true;
        }

        public virtual bool Excluir(TKey id)
        {
            var parametro = new Dictionary<string, object>
            {
                { "ID", id }
            };

            Db.Delete(SqlExcluir, parametro);

            return true;
        }

        public virtual bool Excluir(TEntity entity)
        {
            return Excluir(entity.Id);
        }

        public virtual TEntity SelecionarPorId(TKey id)
        {
            var parametro = new Dictionary<string, object>
            {
                { "ID", id }
            };

            return Db.Get(SqlSelecionarPorId, Converter, parametro);
        }

        public virtual List<TEntity> SelecionarTodos()
        {
            return Db.GetAll(SqlSelecionarTodos, Converter);
        }

        public bool Editar(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
