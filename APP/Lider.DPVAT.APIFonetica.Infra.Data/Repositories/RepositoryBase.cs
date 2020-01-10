
using EFCore.BulkExtensions;
using Lider.DPVAT.APIFonetica.Domain.Interfaces.Repository;
using Lider.DPVAT.APIFonetica.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Lider.DPVAT.APIFonetica.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected EngineContext Db;
        protected DbSet<TEntity> DbSet;

        bool disposed = false;

        public RepositoryBase(EngineContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
      
        public virtual TEntity Add(TEntity obj)
        {
            using (var transaction = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                Db.ChangeTracker.AutoDetectChangesEnabled = false;
                var objreturn = DbSet.Add(obj);
                return objreturn.Entity;
            }
           
        }


        public void AddRange(List<TEntity> obj)
        {

            Db.BulkInsert(obj, new BulkConfig { UseTempDB = false });       

           // Db.SaveChanges();

        }

        public void Update(TEntity obj)
        {
            using (var transaction = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                Db.ChangeTracker.AutoDetectChangesEnabled = false;
                DbSet.Update(obj);
            }

        }

        public void UpdateRange(List<TEntity> obj)
        {    
            Db.BulkUpdate(obj, new BulkConfig { UseTempDB = false });
           
          //  Db.SaveChanges();
                
        }

        public void DeleteRange(List<TEntity> obj)
        {

            //Db.BulkDelete(obj, new BulkConfig { UseTempDB = false });
           Db.RemoveRange(obj);
                 
          //  Db.SaveChanges();
         
        }
        public void Delete(Guid id)
        {

            DbSet.Remove(DbSet.Find(id));
           // Db.SaveChanges();
        }       

        public void Delete(TEntity entity)

        {
            DbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {

            using (var transaction = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                return DbSet.ToList();
            }

        }
        public List<TEntity> ComboBox()
        {
            using (var transaction = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                return DbSet.AsNoTracking().ToList();
            }
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            using (var transaction = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                return DbSet.AsNoTracking();
            }
        }
             
        public TEntity GetById(Guid id)
        {
            using (var transaction = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                var entity = DbSet.Find(id);
                return entity;
            }
        }

        public void RejectChanges()
        {
            foreach (var entry in Db.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Boolean @readonly = true, IList<string> eagerLoads = null, int take = 0)
        {
            var q = DbSet.Where(predicate);

            if (eagerLoads != null)
            {
                foreach (String eager in eagerLoads)
                {
                    q = q.Include(eager);
                }
            }

            var result = new List<TEntity>();

            if (take > 0)
            {
                q = q.Take(take);
            }

            if (@readonly)
            {
                using (var tran = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                {
                    Db.Database.SetCommandTimeout(1000);

                    result = q.AsNoTracking().ToList();

                    return result;
                }
            }
            else
            {
                result = q.ToList();

                foreach (var item in result)
                {
                    Db.Entry<TEntity>(item).Reload();
                }

                return result;
            }
        }

        public int SaveChanges()
        {
            var teste = Db.SaveChanges();

            return teste;
        }

        // Protected implementation of Dispose pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void BeginTrasansaction()
        {
           Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }

        public void Rollback()
        {
            Db.Database.RollbackTransaction();
        }

        public void UpdateBatch(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            Db.Set<TEntity>()
              .Where(filterExpression)
              .Update(updateExpression);
        }
    }
}
