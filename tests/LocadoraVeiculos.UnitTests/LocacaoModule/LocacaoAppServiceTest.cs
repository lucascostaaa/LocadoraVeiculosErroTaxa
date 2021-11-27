using FluentAssertions;
using LocadoraVeiculos.Aplicacao.LocacaoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using LocadoraVeiculos.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using System;

namespace LocadoraVeiculos.UnitTests.LocacaoModule
{
    [TestClass]
    public class LocacaoAppServiceTest
    {
        private Mock<ILocacaoRepository> locacaoRepositoryMock;
        private Mock<IGeradorRelatorioLocacao> geradorRelatorioMock;
        private Mock<IVerificadorConexaoInternet> verificadorInternetMock;
        private Mock<INotificadorEmailLocacao> notificadorEmailMock;
        private Mock<IVeiculoRepository> veiculoRepositoryMock;
        private Mock<Locacao> locacaoMock;
        private LocacaoAppService locacaoService;
        private Locacao locacao;

        public LocacaoAppServiceTest()
        {
            locacaoRepositoryMock = new Mock<ILocacaoRepository>();

            geradorRelatorioMock = new Mock<IGeradorRelatorioLocacao>();

            verificadorInternetMock = new Mock<IVerificadorConexaoInternet>();

            notificadorEmailMock = new Mock<INotificadorEmailLocacao>();

            veiculoRepositoryMock = new Mock<IVeiculoRepository>();

            locacaoMock = new Mock<Locacao>();

            locacaoService = new LocacaoAppService(
                locacaoRepositoryMock.Object, geradorRelatorioMock.Object,
                verificadorInternetMock.Object, notificadorEmailMock.Object, veiculoRepositoryMock.Object);

            locacaoMock.Setup(x => x.Validar())
               .Returns(() =>
               {
                   return "ESTA_VALIDO";
               });

            locacao = locacaoMock.Object;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        [TestMethod]
        public void DeveVerificar_SeLocacao_EstaValida()
        {
            //arrange            
            var locacao = new LocacaoDataBuilder().Build();

            //action
            var resultado = locacaoService.RegistrarNovaLocacao(locacao);

            //assert
            resultado.Should().Be(
                    "Selecione um funcionário" + Environment.NewLine +
                    "Selecione um condutor" + Environment.NewLine +
                    "Selecione um veículo" + Environment.NewLine +
                    "Selecione o plano de cobrança" + Environment.NewLine +
                    "Selecione a data da locação" + Environment.NewLine +
                    "Selecione a data prevista da entrega");
        }

        [TestMethod]
        public void DeveVerificar_InsercaoLocacao_ComProblemas()
        {
            //arrange            
            locacaoRepositoryMock.Setup(x => x.Inserir(locacao)).Returns(false);

            //action
            var resultado = locacaoService.RegistrarNovaLocacao(locacao);

            //assert
            resultado.Should().Be("Locação NÃO registrada. Tivemos problemas com a inserção no banco de dados ");
            geradorRelatorioMock.Verify(x => x.GerarRelatorioPdf(locacao), Times.Never);
        }

        [TestMethod]
        public void DeveAnexar_RelatorioPDF_NaLocacao()
        {
            //arrange                       
            locacaoRepositoryMock.Setup(x => x.Inserir(locacao)).Returns(true);
            geradorRelatorioMock.Setup(x => x.GerarRelatorioPdf(locacao)).Returns(new byte[10]);

            //action
            var resultado = locacaoService.RegistrarNovaLocacao(locacao);

            //assert
            locacao.RelatorioAnexado.Should().Be(true);
            locacaoRepositoryMock.Verify(x => x.Editar(locacao.Id, locacao), Times.Once);
        }

        [TestMethod]
        public void DeveVerificar_GeracaoRelatorio_ComProblemas()
        {
            //arrange                   
            locacaoRepositoryMock.Setup(x => x.Inserir(locacao)).Returns(true);
            geradorRelatorioMock.Setup(x => x.GerarRelatorioPdf(locacao)).Returns(null as byte[]);

            //action
            var resultado = locacaoService.RegistrarNovaLocacao(locacao);

            //assert
            locacao.SituacaoEnvioEmail.Should().Be(SituacaoEnvioEmailEnum.EmailPendente);
            locacaoRepositoryMock.Verify(x => x.Editar(locacao.Id, locacao), Times.Once);
            resultado.Should().Be("Locação registrada, mas tivemos problemas na geração do Relatório PDF ");
        }

        [TestMethod]
        public void DeveEnviar_EmailLocacao_CasoTenhaInternet()
        {
            //arrange                    
            locacaoRepositoryMock.Setup(x => x.Inserir(locacao)).Returns(true);
            locacaoMock.SetupGet(x => x.Condutor.Cliente.Email).Returns("contato@cliente.com.br");

            geradorRelatorioMock.Setup(x => x.GerarRelatorioPdf(locacao)).Returns(new byte[10]);

            verificadorInternetMock.Setup(x => x.TemConexaoComInternet()).Returns(true);

            notificadorEmailMock.Setup(x => x.EnviarEmailLocacao(locacao)).Returns(true);

            //action
            var resultado = locacaoService.RegistrarNovaLocacao(locacao);

            //assert
            notificadorEmailMock.Verify(x => x.EnviarEmailLocacao(locacao));
            locacao.SituacaoEnvioEmail.Should().Be(SituacaoEnvioEmailEnum.EmailEnviado);
            resultado.Should().Be("Locação registrada e o Relatório PDF foi enviado por e-mail ");
            locacaoRepositoryMock.Verify(x => x.Editar(locacao.Id, locacao), Times.Once);
        }

        [TestMethod]
        public void DeveVerificar_EnvioEmailLocacao_CasoNAO_TenhaInternet()
        {
            //arrange                       
            locacaoRepositoryMock.Setup(x => x.Inserir(locacao)).Returns(true);
            geradorRelatorioMock.Setup(x => x.GerarRelatorioPdf(locacao)).Returns(new byte[10]);

            verificadorInternetMock.Setup(x => x.TemConexaoComInternet()).Returns(false);

            //action
            var resultado = locacaoService.RegistrarNovaLocacao(locacao);

            //assert
            notificadorEmailMock.Verify(x => x.EnviarEmailLocacao(locacao), Times.Never);
            locacao.SituacaoEnvioEmail.Should().Be(SituacaoEnvioEmailEnum.EmailPendente);
            resultado.Should().Be("Locação registrada e o Relatório PDF foi enviado por e-mail ");
            locacaoRepositoryMock.Verify(x => x.Editar(locacao.Id, locacao), Times.Once);
        }


    }
}
