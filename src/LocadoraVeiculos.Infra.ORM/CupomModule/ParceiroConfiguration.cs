using LocadoraVeiculos.Dominio.CupomModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.CupomModule
{
    public class ParceiroConfiguration : IEntityTypeConfiguration<Parceiro>
    {
        public void Configure(EntityTypeBuilder<Parceiro> entity)
        {
            entity.ToTable("TBParceiro");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            entity.HasMany(p => p.Cupons)
               .WithOne(p => p.Parceiro)
               .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
