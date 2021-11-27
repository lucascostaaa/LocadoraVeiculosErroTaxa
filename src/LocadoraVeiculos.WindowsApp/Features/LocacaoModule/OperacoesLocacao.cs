using LocadoraVeiculos.Aplicacao.ClienteModule;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Aplicacao.GrupoVeiculoModule;
using LocadoraVeiculos.Aplicacao.LocacaoModule;
using LocadoraVeiculos.Aplicacao.TaxaModule;
using LocadoraVeiculos.Aplicacao.VeiculoModule;
using LocadoraVeiculos.Dominio.ConfiguraoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.LocacaoModule
{
    public class OperacoesLocacao : IOperacoesLocacao
    {
        private TabelaLocacaoControl tabelaLocacoes;

        private readonly ILocacaoAppService locacaoService;

        private readonly IClienteAppService clienteService;
        private readonly IGrupoVeiculoAppService grupoVeiculoService;
        private readonly IVeiculoAppService veiculoService;
        private readonly ITaxaAppService taxaService;
        private readonly ICupomAppService cupomService;

        public OperacoesLocacao(
            ILocacaoAppService locacaoService,
            IClienteAppService clienteService,
            IGrupoVeiculoAppService grupoVeiculoService,
            IVeiculoAppService veiculoService,
            ITaxaAppService taxaService,
            ICupomAppService cupomService
            )
        {
            this.locacaoService = locacaoService;

            this.clienteService = clienteService;
            this.grupoVeiculoService = grupoVeiculoService;
            this.veiculoService = veiculoService;
            this.taxaService = taxaService;
            this.cupomService = cupomService;
        }

        public void InserirNovoRegistro()
        {
            var clientes = clienteService.SelecionarTodos(carregarCondutores: true);

            var gruposVeiculo = grupoVeiculoService.SelecionarTodos(carregarPlanos: true);

            var veiculos = veiculoService.SelecionarTodos(carregarLocacoes: true);

            var taxas = taxaService.SelecionarTodos();

            var cupons = cupomService.SelecionarCuponsAtivos(DateTime.Now.Date);

            var funcionario = TelaPrincipalForm.Instancia.FuncionarioLogado;

            var tela = new TelaRegistroLocacaoForm(funcionario, clientes, gruposVeiculo, veiculos, taxas, cupons);

            if (tela.ShowDialog() == DialogResult.OK)
            {
                var resultado = locacaoService.RegistrarNovaLocacao(tela.Locacao);

                TelaPrincipalForm.Instancia.AtualizarRodape(resultado);

                var registros = locacaoService.SelecionarTodos();

                tabelaLocacoes.AtualizarRegistros(registros);

                Log.Logger.Aqui().FuncionalidadeUsada();
            }
        }

        public void ConcluirOperacao()
        {
            var id = tabelaLocacoes.ObterIdSelecionado();

            if (id == 0)
            {
                MessageBox.Show("Selecione uma locacao para poder realizar a devolução do veículo!", "Devoluções",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var locacao = locacaoService.SelecionarPorId(id);

            var taxasNaoAdicionadas = taxaService.SelecionarTaxasNaoAdicionadas(locacao.TaxasSelecionadas);

            ConfiguracaoCombustivel configuracao = TelaPrincipalForm.Instancia.ConfiguracaoCombustivel;

            var tela = new TelaRegistroDevolucaoForm(locacao, taxasNaoAdicionadas, configuracao);

            if (tela.ShowDialog() == DialogResult.OK)
            {
                var resultado = locacaoService.RegistrarDevolucao(tela.Locacao);

                TelaPrincipalForm.Instancia.AtualizarRodape(resultado);

                var registros = locacaoService.SelecionarTodos();

                tabelaLocacoes.AtualizarRegistros(registros);

                Log.Logger.Aqui().FuncionalidadeUsada();
            }
        }

        public void EditarRegistro()
        {
            var id = tabelaLocacoes.ObterIdSelecionado();

            if (id == 0)
            {
                MessageBox.Show("Selecione uma locacao para poder editar!", "Edição de Locações",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Locacao locacaoSelecionada = locacaoService.SelecionarPorId(id);

            if (locacaoSelecionada.EmAberto == false)
            {
                MessageBox.Show("Não é permitido a edição de locações já concluídas!", "Edição de Locações",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var clientes = clienteService.SelecionarTodos(carregarCondutores: true);

            var gruposVeiculo = grupoVeiculoService.SelecionarTodos(carregarPlanos: true);

            var veiculos = veiculoService.SelecionarTodos(carregarLocacoes: true);

            var taxas = taxaService.SelecionarTodos();

            var cupons = cupomService.SelecionarCuponsAtivos(DateTime.Now.Date);

            var funcionario = TelaPrincipalForm.Instancia.FuncionarioLogado;

            var tela = new TelaRegistroLocacaoForm(funcionario, clientes, gruposVeiculo, veiculos, taxas, cupons);

            tela.Locacao = locacaoSelecionada;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                var resultado = locacaoService.EditarLocacao(tela.Locacao);

                TelaPrincipalForm.Instancia.AtualizarRodape(resultado);

                var registros = locacaoService.SelecionarTodos();

                tabelaLocacoes.AtualizarRegistros(registros);

                Log.Logger.Aqui().FuncionalidadeUsada();
            }
        }

        public void ExcluirRegistro()
        {
            var id = tabelaLocacoes.ObterIdSelecionado();

            if (id == 0)
            {
                MessageBox.Show("Selecione uma locacao para poder exluir!", "Exclusão de Tarefas",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var locacaoSelecionada = locacaoService.SelecionarPorId(id);

            if (MessageBox.Show($"Tem certeza que deseja excluir a locacao: [{locacaoSelecionada.Id}] ?", "Exclusão de Tarefas",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                locacaoService.ExcluirLocacao(locacaoSelecionada);

                var registros = locacaoService.SelecionarTodos();

                tabelaLocacoes.AtualizarRegistros(registros);

                Log.Logger.Aqui().FuncionalidadeUsada();
            }
        }

        public UserControl ObterTabela()
        {
            if (tabelaLocacoes == null)
                tabelaLocacoes = new TabelaLocacaoControl();

            var registros = locacaoService.SelecionarTodos();

            tabelaLocacoes.AtualizarRegistros(registros);

            return tabelaLocacoes;
        }


    }
}
