using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.GrupoVeiculoModule
{
    public class PlanoCobrancaConfiguration : IEntityTypeConfiguration<PlanoCobranca>
    {
        public void Configure(EntityTypeBuilder<PlanoCobranca> entity)
        {
            entity.ToTable("TBPlanoCobranca");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.ValorDia)
                .HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ValorKMRodado)
                .HasColumnType("decimal(18, 0)");

            entity.Property(e => e.KilometragemLivreInclusa)
                .HasColumnName("KilometragemLivreInclusa");

            entity.Property(e => e.TipoPlano).HasConversion<string>();

            entity.HasOne(d => d.GrupoVeiculo)
                .WithMany(p => p.PlanosCobranca)
                .HasForeignKey(d => d.GrupoVeiculoId)
                .HasConstraintName("FK_TBPlanoCobranca_TBGrupoVeiculo");
        }

    }
}