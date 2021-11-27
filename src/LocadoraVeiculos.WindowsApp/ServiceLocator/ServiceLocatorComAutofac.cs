using Autofac;
using LocadoraVeiculos.Aplicacao.ClienteModule;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Aplicacao.GrupoVeiculoModule;
using LocadoraVeiculos.Aplicacao.LocacaoModule;
using LocadoraVeiculos.Aplicacao.TaxaModule;
using LocadoraVeiculos.Aplicacao.VeiculoModule;
using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using LocadoraVeiculos.Infra.InternetServices.LocacaoModule;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.ClienteModule;
using LocadoraVeiculos.Infra.ORM.CupomModule;
using LocadoraVeiculos.Infra.ORM.GrupoVeiculoModule;
using LocadoraVeiculos.Infra.ORM.LocacaoModule;
using LocadoraVeiculos.Infra.ORM.TaxaModule;
using LocadoraVeiculos.Infra.ORM.VeiculoModule;
using LocadoraVeiculos.Infra.PDF.LocacaoModule;
using LocadoraVeiculos.Infra.SQL.ClienteModule;
using LocadoraVeiculos.Infra.SQL.CupomModule;
using LocadoraVeiculos.Infra.SQL.GrupoVeiculoModule;
using LocadoraVeiculos.Infra.SQL.LocacaoModule;
using LocadoraVeiculos.Infra.SQL.TaxaModule;
using LocadoraVeiculos.Infra.SQL.VeiculoModule;
using LocadoraVeiculos.WindowsApp.Features.CupomModule;
using LocadoraVeiculos.WindowsApp.Features.LocacaoModule;

namespace LocadoraVeiculos.WindowsApp.ServiceLocator
{
    public class ServiceLocatorComAutofac : IServiceLocator
    {
        private static IContainer container;

        public T Get<T>()
        {
            return container.Resolve<T>();
        }

        static ServiceLocatorComAutofac()
        {
            var builder = new ContainerBuilder();

            RegistrarOperacoes(builder);

            RegistrarOutros(builder);

            RegistrarServicos(builder);

            //RegistrarRepositoriosSql(builder);

            RegistrarRepositoriosOrm(builder);

            container = builder.Build();
        }

        private static void RegistrarRepositoriosOrm(ContainerBuilder builder)
        {
            builder.RegisterType<LocadoraDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<LocacaoOrmDao>().As<ILocacaoRepository>();
            builder.RegisterType<ClienteOrmDao>().As<IClienteRepository>();
            builder.RegisterType<VeiculoOrmDao>().As<IVeiculoRepository>();
            builder.RegisterType<CupomOrmDao>().As<ICupomRepository>();
            builder.RegisterType<GrupoVeiculoOrmDao>().As<IGrupoVeiculoRepository>();
            builder.RegisterType<TaxaOrmDao>().As<ITaxaRepository>();
            builder.RegisterType<CondutorOrmDao>().As<ICondutorRepository>();
            builder.RegisterType<ParceiroOrmDao>().As<IParceiroRepository>();
        }

        private static void RegistrarRepositoriosSql(ContainerBuilder builder)
        {
            builder.RegisterType<LocacaoSqlDao>().As<ILocacaoRepository>();
            builder.RegisterType<ClienteSqlDao>().As<IClienteRepository>();
            builder.RegisterType<VeiculoSqlDao>().As<IVeiculoRepository>();
            builder.RegisterType<CupomSqlDao>().As<ICupomRepository>();
            builder.RegisterType<GrupoVeiculoSqlDao>().As<IGrupoVeiculoRepository>();
            builder.RegisterType<TaxaSqlDao>().As<ITaxaRepository>();
            builder.RegisterType<CondutorSqlDao>().As<ICondutorRepository>();
        }

        private static void RegistrarServicos(ContainerBuilder builder)
        {
            builder.RegisterType<LocacaoAppService>().As<ILocacaoAppService>();
            builder.RegisterType<ClienteAppService>().As<IClienteAppService>();
            builder.RegisterType<VeiculoAppService>().As<IVeiculoAppService>();
            builder.RegisterType<CupomAppService>().As<ICupomAppService>();
            builder.RegisterType<ParceiroAppService>().As<IParceiroAppService>();
            builder.RegisterType<GrupoVeiculoAppService>().As<IGrupoVeiculoAppService>();
            builder.RegisterType<TaxaAppService>().As<ITaxaAppService>();
        }

        private static void RegistrarOutros(ContainerBuilder builder)
        {
            builder.RegisterType<GeradorRelatorioLocacao>().As<IGeradorRelatorioLocacao>();
            builder.RegisterType<VerificadorConexaoInternet>().As<IVerificadorConexaoInternet>();
            builder.RegisterType<NotificadorEmailLocacao>().As<INotificadorEmailLocacao>();
        }

        private static void RegistrarOperacoes(ContainerBuilder builder)
        {
            builder.RegisterType<OperacoesLocacao>().AsSelf();
            builder.RegisterType<OperacoesCupom>().AsSelf();
            builder.RegisterType<OperacoesParceiro>().AsSelf();
        }
    }
}
