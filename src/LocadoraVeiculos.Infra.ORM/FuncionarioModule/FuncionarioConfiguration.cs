using LocadoraVeiculos.Dominio.FuncionarioModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.FuncionarioModule
{
    public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> entity)
        {
            entity.ToTable("TBFuncionario");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.DataAdmissao).HasColumnType("date");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Salario).HasColumnType("numeric(18, 0)");

            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        }

    }
}
