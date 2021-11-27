using FluentAssertions;
using LocadoraVeiculos.Dominio.CupomModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocadoraVeiculos.IntegrationTests.CupomModule
{
    [TestClass]
    public class ParceiroOrmDaoTest : IntegrationTestBase
    {
        private Parceiro parceiro;

        [TestMethod]
        public void DeveInserir_Parceiro()
        {
            Parceiro parceiro = new Parceiro("Deko");

            parceiroRepository.Inserir(parceiro);

            parceiro.Id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void DeveAtualizar_Parceiro()
        {
            parceiro = new Parceiro("Deko");
            parceiroRepository.Inserir(parceiro);

            var parceiroAtualizado = new Parceiro("Radio Jovem Pan");

            parceiroRepository.Editar(parceiro.Id, parceiroAtualizado);

            var parceiroEncontrado = parceiroRepository.SelecionarPorId(parceiro.Id);
            parceiroAtualizado.Should().Be(parceiroEncontrado);
        }

        [TestMethod]
        public void DeveExcluir_Parceiro_E_NaoDeveExcluir_Cupons()
        {
            parceiro = new Parceiro("Deko");
            parceiroRepository.Inserir(parceiro);

            Cupom cupom1 = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), parceiro, 100, TipoCupomEnum.ValorFixo);

            Cupom cupom2 = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), parceiro, 100, TipoCupomEnum.ValorFixo);

            cupomRepository.Inserir(cupom1);
            cupomRepository.Inserir(cupom2);

            parceiroRepository.Excluir(parceiro.Id);

            var parceiroEncontrado = parceiroRepository.SelecionarPorId(parceiro.Id);
            parceiroEncontrado.Should().BeNull();
            var cupons = cupomRepository.SelecionarTodos();
            cupons.Count.Should().Be(2);
            cupons[0].Parceiro.Should().BeNull();
            cupons[1].Parceiro.Should().BeNull();
        }

        [TestMethod]
        public void DeveSelecionar_ParceiroComCupons()
        {
            Parceiro parceiro = new Parceiro("Deko");

            parceiroRepository.Inserir(parceiro);

            Cupom cupom1 = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), parceiro, 100, TipoCupomEnum.ValorFixo);

            Cupom cupom2 = new Cupom("Dez conto de desconto", 10, new DateTime(2021, 12, 31), parceiro, 100, TipoCupomEnum.ValorFixo);

            cupomRepository.Inserir(cupom1);
            cupomRepository.Inserir(cupom2);

            var parceiroSelecionado = parceiroRepository.SelecionarPorId(parceiro.Id);

            parceiroSelecionado.Id.Should().BeGreaterThan(0);
            parceiroSelecionado.Cupons.Count.Should().Be(2);
        }

        [TestMethod]
        public void DeveSelecionar_Parceiros()
        {
            Parceiro parceiro1 = new Parceiro("Deko");
            Parceiro parceiro2 = new Parceiro("Radio Jovem Pan");

            parceiroRepository.Inserir(parceiro1);
            parceiroRepository.Inserir(parceiro2);

            var parceiros = parceiroRepository.SelecionarTodos();

            parceiros.Count.Should().Be(2);
        }
    }
}
