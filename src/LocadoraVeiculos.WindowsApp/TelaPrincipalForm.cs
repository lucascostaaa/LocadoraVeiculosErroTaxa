using LocacaoVeiculos.WindowsApp.Shared;
using LocadoraVeiculos.Dominio.ConfiguraoModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.WindowsApp.Features.CupomModule;
using LocadoraVeiculos.WindowsApp.Features.LocacaoModule;
using LocadoraVeiculos.WindowsApp.ServiceLocator;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp
{
    public partial class TelaPrincipalForm : Form
    {
        public static TelaPrincipalForm Instancia;

        private readonly Funcionario funcionario;
        private readonly IServiceLocator serviceLocator;
        private ICadastravel operacoes;

        public TelaPrincipalForm()
        {
            InitializeComponent();
            Instancia = this;
        }

        public TelaPrincipalForm(Funcionario funcionario, IServiceLocator serviceLocator)
            : this()
        {
            this.funcionario = funcionario;
            this.serviceLocator = serviceLocator;
        }


        public Funcionario FuncionarioLogado { get => funcionario; }

        public ConfiguracaoCombustivel ConfiguracaoCombustivel
        {
            get
            {
                return new ConfiguracaoCombustivel
                {
                    ValorGasolina = 5.5m,
                    ValorAlcool = 4.5m,
                    ValorDiesel = 3.5m
                };
            }
        }

        private void locacoesVeiculosMenuItem_Click(object sender, System.EventArgs e)
        {
            operacoes = serviceLocator.Get<OperacoesLocacao>();

            ConfigurarBarraTarefas(new ConfiguracaoLocacaoToolbox());

            ConfigurarVisualizacaoRegistros(operacoes);
        }


        private void parceirosMenuItem_Click(object sender, EventArgs e)
        {
            operacoes = serviceLocator.Get<OperacoesParceiro>();

            ConfigurarBarraTarefas(new ConfiguracaoParceiroToolbox());

            ConfigurarVisualizacaoRegistros(operacoes);
        }

        private void cuponsMenuItem_Click(object sender, EventArgs e)
        {
            operacoes = serviceLocator.Get<OperacoesCupom>();

            ConfigurarBarraTarefas(new ConfiguracaoCupomToolbox());

            ConfigurarVisualizacaoRegistros(operacoes);
        }

        private void ConfigurarVisualizacaoRegistros(ICadastravel operacoes)
        {
            UserControl tabela = operacoes.ObterTabela();

            tabela.Dock = DockStyle.Fill;

            panelRegistros.Controls.Clear();

            panelRegistros.Controls.Add(tabela);
        }

        private void ConfigurarBarraTarefas(IConfiguracaoToolbox configuracao)
        {
            toolboxAcoes.Enabled = true;

            labelTipoCadastro.Text = configuracao.ObtemDescricao();

            btnInserirNovo.ToolTipText = configuracao.ObtemToolTips().Adicionar;
            btnEditar.ToolTipText = configuracao.ObtemToolTips().Editar;
            btnExcluir.ToolTipText = configuracao.ObtemToolTips().Excluir;
            btnConcluir.ToolTipText = configuracao.ObtemToolTips().Concluir;

            btnInserirNovo.Enabled = configuracao.ObtemEstadoBotoes().Adicionar;
            btnEditar.Enabled = configuracao.ObtemEstadoBotoes().Editar;
            btnExcluir.Enabled = configuracao.ObtemEstadoBotoes().Excluir;
            btnConcluir.Enabled = configuracao.ObtemEstadoBotoes().Concluir;
            btnEnviarEmail.Enabled = configuracao.ObtemEstadoBotoes().Enviar;
        }

        private void btnInserirNovo_Click(object sender, EventArgs e)
        {
            operacoes.InserirNovoRegistro();
        }

        private void btnConcluir_Click(object sender, EventArgs e)
        {
            ((IOperacoesLocacao)operacoes).ConcluirOperacao();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            operacoes.EditarRegistro();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            operacoes.ExcluirRegistro();
        }

        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }


    }
}
