using LocadoraVeiculos.Dominio.VeiculoModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.VeiculoModule
{
    public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> entity)
        {
            entity.ToTable("TBVeiculo");

            entity.HasKey(e => e.Id);


            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Fabricante)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Imagem).HasColumnType("image");

            entity.Property(e => e.Modelo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NumeroChassi)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Placa)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TamanhoPortaMalas)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.QtdLitrosTanque);

            entity.Property(e => e.TipoCombustivel)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(e => e.GrupoVeiculo)
                .WithMany(g => g.Veiculos)
                .HasForeignKey(e => e.GrupoVeiculoId)
                .HasConstraintName("FK_TBVeiculo_TBGrupoVeiculo");
        }

    }
}