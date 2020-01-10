using Lider.DPVAT.APIFonetica.Domain.Entities;
using Lider.DPVAT.APIFonetica.Domain.Interfaces.Repository;
using Lider.DPVAT.APIFonetica.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lider.DPVAT.APIFonetica.Infra.Data.Repositories
{
    public class LogFoneticaRepository : RepositoryBase<TbLogFonetica>, ILogFoneticaRepository, IDisposable
    {
        protected EngineContext Db;
        protected DbSet<TbLogFonetica> Dbset;

        bool disposed = false;
        public LogFoneticaRepository(EngineContext context) : base(context)
        {
            Db = context;
            Dbset = Db.Set<TbLogFonetica>();
        }
    

        public TbLogFonetica GetTbLogFonetica(int id)
        {
            using (var transaction = Db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                return Db.TbLogFonetica.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }
        }
    }
}
