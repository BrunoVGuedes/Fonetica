using Microsoft.EntityFrameworkCore;
using PSLAdministrativa.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Text;
using Lider.DPVAT.APIFonetica.Domain.Entities;

namespace Lider.DPVAT.APIFonetica.Infra.Data.Context
{
    public class EngineContext : DbContext
    {
        public EngineContext(DbContextOptions<EngineContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(150000);            
        }

        #region DbSets
        public virtual DbSet<TbLog> TbLog { get; set; }
        public virtual DbSet<TbLogFonetica> TbLogFonetica { get; set; }
        public virtual DbSet<TaTipoCodRetorno> TaTipoCodRetorno { get; set; }

        #endregion

        #region Configurações
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configurações Externas
            modelBuilder.ApplyConfiguration(new TbLogConfig());
            modelBuilder.ApplyConfiguration(new TbLogFoneticaConfig());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
