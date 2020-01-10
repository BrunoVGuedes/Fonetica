using Lider.DPVAT.APIFonetica.Domain.Entities;
using Lider.DPVAT.APIFonetica.Domain.Interfaces;
using Lider.DPVAT.APIFonetica.Domain.Interfaces.Repository;
using Lider.DPVAT.APIFonetica.Infra.Data.Context;
using Lider.DPVAT.APIFonetica.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lider.DPVAT.APIFonetica.Infra.Data.UnityOfWorks
{
        public class UnitOfWork : IUnitOfWork
        {
        private readonly EngineContext _context;

        public bool _disposed;

        public UnitOfWork(EngineContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
           // _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            _context.Database.RollbackTransaction();

        }
    }
}
