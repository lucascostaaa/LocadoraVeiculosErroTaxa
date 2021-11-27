using Autofac;
using Autofac.Extensions.DependencyInjection;
using LocadoraVeiculos.Aplicacao.LocacaoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.LocacaoModule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocadoraVeiculos.WindowsServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {

                builder.RegisterType<LocacaoAppService>().As<ILocacaoAppService>();

                builder.RegisterType<LocadoraDbContext>().InstancePerLifetimeScope();

                builder.RegisterType<LocacaoOrmDao>().As<ILocacaoRepository>();

            })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<EnvioEmailWorker>();
                });
    }
}
