using LocadoraVeiculos.Dominio.LocacaoModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraVeiculos.Infra.ORM.LocacaoModule
{
    public partial class LocacaoConfiguration : IEntityTypeConfiguration<Locacao>
    {
        public void Configure(EntityTypeBuilder<Locacao> entity)
        {
            entity.ToTable("TBLocacao");

            entity.HasKey(e => e.Id);

            entity.Ignore(e => e.RegistrandoDevolucao);

            entity.Property(e => e.DataDevolucaoPrevista).HasColumnType("date");

            entity.Property(e => e.DataDevolucaoRealizada).HasColumnType("date");

            entity.Property(e => e.DataLocacao).HasColumnType("date");

            entity.Property(e => e.EmAberto);

            entity.Property(e => e.QuilometragemPercorrida).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.MarcadorCombustivel).HasConversion<string>();

            entity.Property(e => e.SituacaoEnvioEmail).HasConversion<string>();


            entity.HasOne(d => d.Cupom)
                .WithMany(c => c.Locacoes)
                .HasForeignKey(d => d.CupomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBLocacao_TBCupom");

            entity.HasOne(d => d.Funcionario)
                .WithMany()
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TBLocacao_TBFuncionario");

            entity.HasOne(d => d.PlanoCobranca)
                .WithMany()
                .HasForeignKey(d => d.PlanoCobrancaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TBLocacao_TBPlanoCobranca");

            entity.HasOne(d => d.Condutor)
                .WithMany()
                .HasForeignKey(d => d.CondutorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TBLocacao_TBCondutor");

            entity.HasOne(d => d.Veiculo)
                .WithMany(p => p.Locacoes)
                .HasForeignKey(d => d.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TBLocacao_TBVeiculos");

            entity.HasMany(e => e.TaxasSelecionadas);
        }
    }
}
