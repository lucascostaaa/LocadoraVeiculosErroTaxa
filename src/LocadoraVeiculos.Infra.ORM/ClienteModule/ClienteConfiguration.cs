using LocadoraVeiculos.Dominio.ClienteModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.ClienteModule
{
    public partial class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            entity.ToTable("TBCliente");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CNPJ)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CNPJ");

            entity.Property(e => e.CPF)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CPF");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Endereco)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.RG)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("RG");

            entity.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.TipoPessoa)
                .IsRequired()
                .HasConversion<string>();

        }
    }
}
