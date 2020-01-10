using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lider.DPVAT.APIFonetica.Domain.Entities;

namespace PSLAdministrativa.Data.EntityConfig
{
    public class TbLogFoneticaConfig : IEntityTypeConfiguration<TbLogFonetica>
    {
        public void Configure(EntityTypeBuilder<TbLogFonetica> entity)
        {
            entity.ToTable("TB_Log_Fonetica")
                .HasKey(e => e.Id);

             entity.HasOne(p => p.TaTipoCodRetorno)
                   .WithMany(p => p.TbLogFonetica)
                   .HasForeignKey(p => p.CD_Retorno);

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DT_Cadastro)
                .HasColumnName("DT_Cadastro");

            entity.Property(e => e.Palavra)
                .HasColumnName("NM_Palavra");

            entity.Property(e => e.Fonetica)
                .HasColumnName("NM_Fonetica");
            
            entity.Property(e => e.DT_Retorno)
                .HasColumnName("DT_Retorno");

            entity.Property(e => e.CD_Retorno)
                .HasColumnName("CD_Retorno");            

        }
    }
}
