using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Infra.ORM
{
    public class RepositoryOrmBase<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey>
        where TEntity : EntidadeBase<TKey>, new()
    {
        protected readonly LocadoraDbContext db;
        protected readonly DbSet<TEntity> dbSet;

        public RepositoryOrmBase(LocadoraDbContext dbContext)
        {
            db = dbContext;
            dbSet = db.Set<TEntity>();
        }

        public virtual bool Inserir(TEntity entity)
        {
            try
            {
                dbSet.Add(entity);

                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public virtual bool Editar(TEntity entityNewValues)
        {
            try
            {
                db.Update(entityNewValues);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public virtual bool Editar(TKey id, TEntity entityNewValues)
        {
            try
            {
                TEntity entityForUpdate = dbSet.SingleOrDefault(x => x.Id.Equals(id));

                entityNewValues.Id = id;

                db.Entry(entityForUpdate).CurrentValues.SetValues(entityNewValues);

                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public virtual bool Excluir(TKey id)
        {
            try
            {
                TEntity entityForDelete = SelecionarPorId(id);

                db.Remove(entityForDelete);

                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public virtual TEntity SelecionarPorId(TKey id)
        {
            return dbSet.SingleOrDefault(x => x.Id.Equals(id));
        }

        public virtual List<TEntity> SelecionarTodos()
        {
            return dbSet.ToList();
        }

        public virtual bool Excluir(TEntity entity)
        {
            dbSet.Remove(entity);

            db.SaveChanges();

            return true;
        }
    }
}
