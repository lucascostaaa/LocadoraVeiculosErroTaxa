using Autofac;
using AutoMapper;
using LocadoraVeiculos.Aplicacao.ClienteModule;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Aplicacao.FuncionarioModule;
using LocadoraVeiculos.Aplicacao.GrupoVeiculoModule;
using LocadoraVeiculos.Aplicacao.LocacaoModule;
using LocadoraVeiculos.Aplicacao.TaxaModule;
using LocadoraVeiculos.Aplicacao.VeiculoModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.Shared;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using LocadoraVeiculos.Infra.InternetServices.LocacaoModule;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.ClienteModule;
using LocadoraVeiculos.Infra.ORM.CupomModule;
using LocadoraVeiculos.Infra.ORM.FuncionarioModule;
using LocadoraVeiculos.Infra.ORM.GrupoVeiculoModule;
using LocadoraVeiculos.Infra.ORM.LocacaoModule;
using LocadoraVeiculos.Infra.ORM.TaxaModule;
using LocadoraVeiculos.Infra.ORM.VeiculoModule;
using LocadoraVeiculos.Infra.PDF.LocacaoModule;

namespace LocadoraVeiculos.WebApi.Config
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegistrarOutros(builder);

            RegistrarServicos(builder);

            RegistrarRepositoriosOrm(builder);

            builder.RegisterType<Mapper>().As<IMapper>();
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
            builder.RegisterType<FuncionarioOrmDao>().As<IFuncionarioRepository>();
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
            builder.RegisterType<FuncionarioAppService>().As<IFuncionarioAppService>();
        }

           private static void RegistrarOutros(ContainerBuilder builder)
           {
               builder.RegisterType<GeradorRelatorioLocacao>().As<IGeradorRelatorioLocacao>();
               builder.RegisterType<VerificadorConexaoInternet>().As<IVerificadorConexaoInternet>();
               builder.RegisterType<NotificadorEmailLocacao>().As<INotificadorEmailLocacao>();

               builder.RegisterType<Notificador>().As<INotificador>().InstancePerLifetimeScope();
    
        }

       

    }
}
