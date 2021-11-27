using Autofac;
using AutoMapper;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Aplicacao.FuncionarioModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.Shared;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.CupomModule;
using LocadoraVeiculos.Infra.ORM.FuncionarioModule;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace LocadoraVeiculos.WebApplication.AutofacConfig
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocadoraDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<ParceiroOrmDao>().As<IParceiroRepository>();

            builder.RegisterType<CupomOrmDao>().As<ICupomRepository>();

            builder.RegisterType<FuncionarioOrmDao>().As<IFuncionarioRepository>();

            builder.RegisterType<ParceiroAppService>().As<IParceiroAppService>();

            builder.RegisterType<CupomAppService>().As<ICupomAppService>();

            builder.RegisterType<FuncionarioAppService>().As<IFuncionarioAppService>();

            builder.RegisterType<Mapper>().As<IMapper>();

            builder.RegisterType<Notificador>().As<INotificador>().InstancePerLifetimeScope();

        }
    }
}