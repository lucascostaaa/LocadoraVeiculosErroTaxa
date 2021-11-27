using LocadoraVeiculos.Dominio.TaxaModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.TaxaModule
{
    public partial class TaxaConfiguration : IEntityTypeConfiguration<Taxa>
    {
        public void Configure(EntityTypeBuilder<Taxa> entity)
        {
            entity.ToTable("TBTaxa");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.TipoTaxa).HasConversion<string>();

            entity.HasMany(e => e.Locacoes);
        }
    }
}