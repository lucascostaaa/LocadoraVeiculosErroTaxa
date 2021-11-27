using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace LocadoraVeiculos.Infra.ORM
{
    public class LocadoraDbContext : DbContext
    {


        private static readonly ILoggerFactory SerilogLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Debug)
                    .AddDebug()
                    .AddSerilog(Log.Logger, dispose: true);
            });


        public DbSet<Parceiro> Parceiros { get; set; }

        public DbSet<Cupom> Cupons { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Condutor> Condutores { get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<Taxa> Taxas { get; set; }

        public DbSet<Locacao> Locacoes { get; set; }

        public DbSet<GrupoVeiculo> GrupoVeiculos { get; set; }

        public DbSet<PlanoCobranca> PlanosCombranca { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(SerilogLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=DbLocadora_Novo;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocadoraDbContext).Assembly);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}