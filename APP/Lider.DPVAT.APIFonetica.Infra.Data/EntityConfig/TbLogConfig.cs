using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lider.DPVAT.APIFonetica.Domain.Entities;

namespace PSLAdministrativa.Data.EntityConfig
{
    public class TbLogConfig : IEntityTypeConfiguration<TbLog>
    {
        public void Configure(EntityTypeBuilder<TbLog> entity)
        {
            entity.ToTable("TB_Log");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            entity.Property(e => e.DeClasse)
                .HasColumnName("DE_Classe")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DeDadoEntrada)
                .HasColumnName("DE_Dado_Entrada")
                .IsUnicode(false);

            entity.Property(e => e.DeDadoSaida)
                .HasColumnName("DE_Dado_Saida")
                .IsUnicode(false);

            entity.Property(e => e.DeLog)
                .HasColumnName("DE_Log")
                .IsUnicode(false);

            entity.Property(e => e.DeMetodo)
                .HasColumnName("DE_Metodo")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DeTipoLog)
                .HasColumnName("DE_Tipo_Log")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DtLog)
                .HasColumnName("DT_Log")
                .HasColumnType("datetime");
        }
    }
}
