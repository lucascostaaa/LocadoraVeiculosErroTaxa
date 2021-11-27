using FluentAssertions;
using LocadoraVeiculos.Dominio.CupomModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocadoraVeiculos.IntegrationTests.CupomModule
{
    [TestClass]
    public class CupomOrmDaoTest : IntegrationTestBase
    {
        [TestMethod]
        public void DeveInserir_Cupom()
        {
            Cupom cupom = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), Deko(), 100, TipoCupomEnum.ValorFixo);

            cupomRepository.Inserir(cupom);

            cupom.Id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void DeveAtualizar_Cupom()
        {
            var cupom = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), Deko(), 100, TipoCupomEnum.ValorFixo);

            cupomRepository.Inserir(cupom);

            var cupomAtualizado = new Cupom("Meu desconto", 50, new DateTime(2021, 12, 31), RadioClube(), 300, TipoCupomEnum.ValorFixo);

            cupomRepository.Editar(cupom.Id, cupomAtualizado);

            var cupomSelecionado = cupomRepository.SelecionarPorId(cupom.Id);

            cupomAtualizado.Should().Be(cupomSelecionado);
        }

        [TestMethod]
        public void DeveExcluir_Cupom()
        {
            var cupom = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), Deko(), 100, TipoCupomEnum.ValorFixo);

            cupomRepository.Inserir(cupom);

            cupomRepository.Excluir(cupom.Id);

            var cupomSelecionado = cupomRepository.SelecionarPorId(cupom.Id);

            cupomSelecionado.Should().BeNull();

            var parceiros = parceiroRepository.SelecionarTodos();
            parceiros.Count.Should().Be(1);
        }

        [TestMethod]
        public void DeveSelecionar_CupomComParceiro()
        {
            var cupom = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), Deko(), 100, TipoCupomEnum.ValorFixo);

            cupomRepository.Inserir(cupom);

            var cupomSelecionado = cupomRepository.SelecionarPorId(cupom.Id, carregarParceiro: true);

            cupomSelecionado.Parceiro.Should().NotBeNull();
        }

        private Parceiro Deko()
        {
            var deko = new Parceiro("Desconto do Deko");

            parceiroRepository.Inserir(deko);

            return deko;
        }

        private Parceiro RadioClube()
        {
            var radioClube = new Parceiro("Radio Clube");

            parceiroRepository.Inserir(radioClube);

            return radioClube;
        }
    }
}
