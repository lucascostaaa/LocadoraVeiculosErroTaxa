using FluentAssertions;
using LocadoraVeiculos.Aplicacao.ClienteModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.ClienteModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LocadoraVeiculos.UnitTests.ClienteModule
{
    [TestClass]
    public class ClienteAppServiceTest
    {
        private readonly Mock<Cliente> clienteMock;
        private readonly Mock<IClienteRepository> clienteRepomock;

        private readonly ClienteAppService sut; //system under test 

        public ClienteAppServiceTest()
        {
            clienteMock = new Mock<Cliente>();
            clienteRepomock = new Mock<IClienteRepository>();

            sut = new ClienteAppService(clienteRepomock.Object);
        }

        [TestMethod]
        public void NaoDeve_CadastrarCliente_ComCpfRepetido()
        {
            //arrange
            clienteRepomock.Setup(x => x.ExisteClienteComEsteCpf("123"))
                .Returns(() =>
                {
                    return true;
                });

            var novoCliente = new Cliente("Rech", "", "", "321", "123", "", TipoPessoaEnum.Fisica, "contato@gmail.com");

            //action
            var resultado = sut.InserirNovoCliente(novoCliente);

            //assert
            resultado.Should().Be("Este CPF já está cadastrado");
        }

        [TestMethod]
        public void Deve_CadastrarCliente_Valido()
        {
            //arrange
            clienteMock.Setup(x => x.Validar())
                .Returns(() =>
                    { return "ESTA_VALIDO"; }
                );

            clienteMock.SetupGet(x => x.CPF)
                .Returns(() =>
                    { return "123"; }
                );

            clienteRepomock.Setup(x => x.ExisteClienteComEsteCpf("123"))
               .Returns(() =>
               {
                   return false;
               });

            Cliente cliente = clienteMock.Object;

            //action
            var resultado = sut.InserirNovoCliente(cliente);

            //assert
            clienteRepomock.Verify(x => x.Inserir(cliente));
        }
    }
}
