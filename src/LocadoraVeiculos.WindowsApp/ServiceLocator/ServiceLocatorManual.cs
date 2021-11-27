using LocacaoVeiculos.WindowsApp.Shared;
using LocadoraVeiculos.Aplicacao.ClienteModule;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Aplicacao.GrupoVeiculoModule;
using LocadoraVeiculos.Aplicacao.LocacaoModule;
using LocadoraVeiculos.Aplicacao.TaxaModule;
using LocadoraVeiculos.Aplicacao.VeiculoModule;
using LocadoraVeiculos.Dominio;
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
using LocadoraVeiculos.WindowsApp.Features.CupomModule;
using LocadoraVeiculos.WindowsApp.Features.LocacaoModule;
using System;
namespace LocadoraVeiculos.WindowsApp.ServiceLocator
{
    public class ServiceLocatorManual : IServiceLocator
    {
        public T Get<T>()
        {
            var type = typeof(T).Name;

            if (type == "OperacoesLocacao")
                return (T)GetOperacoesLocacao();

            else if (type == "OperacoesCupom")
                return (T)GetOperacoesCupom();

            else if (type == "OperacoesParceiro")
                return (T)GetOperacoesParceiro();

            else
                throw new ApplicationException("A operação solicitada não esta registrada.");
        }

        private ICadastravel GetOperacoesParceiro()
        {
            LocadoraDbContext dbContext = new LocadoraDbContext();

            INotificador notificador = new Notificador();

            IParceiroRepository parceiroRepository = new ParceiroOrmDao(dbContext);

            IParceiroAppService parceiroService = new ParceiroAppService(parceiroRepository, notificador);

            OperacoesParceiro operacoesParceiro = new OperacoesParceiro(parceiroService, parceiroRepository, notificador);

            return operacoesParceiro;
        }

        private ICadastravel GetOperacoesCupom()
        {
            LocadoraDbContext dbContext = new LocadoraDbContext();

            ICupomRepository cupomRepository = new CupomOrmDao(dbContext);
            IParceiroRepository parceiroRepository = new ParceiroOrmDao(dbContext);

            INotificador notificador = new Notificador();
            ICupomAppService cupomService = new CupomAppService(cupomRepository, notificador);
            IParceiroAppService parceiroService = new ParceiroAppService(parceiroRepository, notificador);

            OperacoesCupom opCupom = new OperacoesCupom(cupomService, parceiroService, notificador);

            return opCupom;
        }

        private ICadastravel GetOperacoesLocacao()
        {
            LocadoraDbContext dbContext = new LocadoraDbContext();

            INotificador notificador = new Notificador();

            IClienteRepository clienteRepository = new ClienteOrmDao(dbContext);

            IGrupoVeiculoRepository grupoVeiculoRepository = new GrupoVeiculoOrmDao(dbContext);

            ITaxaRepository taxaRepository = new TaxaOrmDao(dbContext);

            ICupomRepository cupomRepository = new CupomOrmDao(dbContext);

            ILocacaoRepository locacaoRepository = new LocacaoOrmDao(dbContext);

            IVeiculoRepository veiculoRepository = new VeiculoOrmDao(dbContext);

            IGeradorRelatorioLocacao geradorRelatorio = new GeradorRelatorioLocacao();

            IVerificadorConexaoInternet verificadorInternet = new VerificadorConexaoInternet();

            INotificadorEmailLocacao notificadorEmail = new NotificadorEmailLocacao();

            IClienteAppService clienteService = new ClienteAppService(clienteRepository);

            IGrupoVeiculoAppService grupoVeiculoService = new GrupoVeiculoAppService(grupoVeiculoRepository);

            ITaxaAppService taxaService = new TaxaAppService(taxaRepository, notificador);

            ICupomAppService cupomService = new CupomAppService(cupomRepository, notificador);

            IVeiculoAppService veiculoService = new VeiculoAppService(veiculoRepository);

            LocacaoAppService locacaoService = new LocacaoAppService(
                locacaoRepository,
                geradorRelatorio,
                verificadorInternet,
                notificadorEmail,
                veiculoRepository);

            OperacoesLocacao operacoesLocacao = new OperacoesLocacao(
                locacaoService,
                clienteService,
                grupoVeiculoService,
                veiculoService,
                taxaService,
                cupomService);

            return operacoesLocacao;
        }


    }
}