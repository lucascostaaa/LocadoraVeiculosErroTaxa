using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.ClienteModule;
using LocadoraVeiculos.Infra.ORM.CupomModule;
using LocadoraVeiculos.Infra.ORM.FuncionarioModule;
using LocadoraVeiculos.Infra.ORM.GrupoVeiculoModule;
using LocadoraVeiculos.Infra.ORM.LocacaoModule;
using LocadoraVeiculos.Infra.ORM.TaxaModule;
using LocadoraVeiculos.Infra.ORM.VeiculoModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocadoraVeiculos.IntegrationTests
{
    public class IntegrationTestBase
    {
        private LocadoraDbContext dbContext;

        protected ILocacaoRepository locacaoRepository;
        protected IParceiroRepository parceiroRepository;
        protected ICupomRepository cupomRepository;
        protected ITaxaRepository taxaRepository;
        protected IFuncionarioRepository funcionarioRepository;
        protected IClienteRepository clienteRepository;
        protected ICondutorRepository condutorRepository;
        protected IVeiculoRepository veiculoRepository;
        protected IGrupoVeiculoRepository grupoVeiculoRepository;

        public IntegrationTestBase()
        {
            dbContext = new LocadoraDbContext();

            locacaoRepository = new LocacaoOrmDao(dbContext);
            parceiroRepository = new ParceiroOrmDao(dbContext);
            cupomRepository = new CupomOrmDao(dbContext);
            taxaRepository = new TaxaOrmDao(dbContext);
            funcionarioRepository = new FuncionarioOrmDao(dbContext);
            condutorRepository = new CondutorOrmDao(dbContext);
            clienteRepository = new ClienteOrmDao(dbContext);
            veiculoRepository = new VeiculoOrmDao(dbContext);
            grupoVeiculoRepository = new GrupoVeiculoOrmDao(dbContext);
        }


        [TestCleanup]
        public void TearDown()
        {
            using LocadoraDbContext dbContext = new LocadoraDbContext();

            dbContext.Locacoes.RemoveRange(dbContext.Locacoes);

            dbContext.Taxas.RemoveRange(dbContext.Taxas);

            dbContext.Cupons.RemoveRange(dbContext.Cupons);

            dbContext.Parceiros.RemoveRange(dbContext.Parceiros);

            dbContext.Veiculos.RemoveRange(dbContext.Veiculos);

            dbContext.PlanosCombranca.RemoveRange(dbContext.PlanosCombranca);

            dbContext.GrupoVeiculos.RemoveRange(dbContext.GrupoVeiculos);

            dbContext.Condutores.RemoveRange(dbContext.Condutores);

            dbContext.Clientes.RemoveRange(dbContext.Clientes);

            dbContext.Funcionarios.RemoveRange(dbContext.Funcionarios);

            dbContext.SaveChanges();
        }

    }
}
