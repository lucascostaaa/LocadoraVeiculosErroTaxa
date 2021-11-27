using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.GrupoVeiculoModule
{
    public class GrupoVeiculoConfiguration : IEntityTypeConfiguration<GrupoVeiculo>
    {
        public void Configure(EntityTypeBuilder<GrupoVeiculo> entity)
        {
            entity.ToTable("TBGrupoVeiculo");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasMany(e => e.Veiculos);

            entity.HasMany(e => e.PlanosCobranca);

        }
    }


}
