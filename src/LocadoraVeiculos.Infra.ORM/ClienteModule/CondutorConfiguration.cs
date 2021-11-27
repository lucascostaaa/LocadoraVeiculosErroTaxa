using LocadoraVeiculos.Dominio.ClienteModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.ClienteModule
{
    public class CondutorConfiguration : IEntityTypeConfiguration<Condutor>
    {
        public void Configure(EntityTypeBuilder<Condutor> entity)
        {
            entity.ToTable("TBCondutor");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Cnh)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CNH");

            entity.Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CPF");

            entity.Property(e => e.DataValidadeCnh)
                .HasColumnType("date")
                .HasColumnName("DataValidadeCNH");

            entity.Property(e => e.Endereco)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Rg)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("RG");

            entity.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente)
                .WithMany(p => p.Condutores)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TBCondutor_TBCliente");
        }
    }
}