using LocadoraVeiculos.Dominio.ConfiguraoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.LocacaoModule
{
    public partial class TelaRegistroDevolucaoForm : Form
    {
        private bool preenchendoCampos;

        private Locacao locacao;

        private readonly List<Taxa> taxasNaoAdicionadas;
        private readonly ConfiguracaoCombustivel configuracaoCombustivel;

        public TelaRegistroDevolucaoForm()
        {
            InitializeComponent();
            labelValorTotal.Text = "";
        }

        public TelaRegistroDevolucaoForm(
            Locacao locacao,
            List<Taxa> taxasNaoAdicionadas,
            ConfiguracaoCombustivel configuracaoCombustivel) : this()
        {
            this.locacao = locacao;
            this.taxasNaoAdicionadas = taxasNaoAdicionadas;
            this.configuracaoCombustivel = configuracaoCombustivel;

            PreencherCamposTela();
        }

        private void PreencherCamposTela()
        {
            preenchendoCampos = true;

            txtId.Text = locacao.Id.ToString();
            txtFuncionario.Text = locacao.Funcionario.Nome;
            txtCliente.Text = locacao.Condutor.Cliente.Nome;
            txtCondutor.Text = locacao.Condutor.Nome;
            txtGrupoVeiculo.Text = locacao.Veiculo.GrupoVeiculo.Nome;
            txtVeiculo.Text = locacao.Veiculo.Modelo;
            txtPlano.Text = locacao.PlanoCobranca.TipoPlano.GetDescription();
            txtKmVeiculo.Text = locacao.Veiculo.Quilometragem.ToString();
            txtDataLocacao.Text = locacao.DataLocacao.ToString();
            txtDevolucaoPrevista.Text = locacao.DataDevolucaoPrevista.ToString();
            txtDataDevolucao.Text = locacao.DataDevolucaoPrevista.ToString();

            if (locacao.TemCupom())
                txtCupom.Text = locacao.Cupom.Nome;

            foreach (var item in locacao.TaxasSelecionadas)
                listBoxTaxasSelecionadas.Items.Add(item);

            foreach (var item in taxasNaoAdicionadas)
                listTaxas.Items.Add(item);

            preenchendoCampos = false;

            ConfigurarDevolucao();
        }


        public Locacao Locacao { get { return locacao; } }

        private void ValueChanged(object sender, EventArgs e)
        {
            if (preenchendoCampos)
                return;

            ConfigurarDevolucao();
        }

        private void ConfigurarDevolucao()
        {
            if (cmbNivelTanque.SelectedItem != null)
                locacao.MarcadorCombustivel = ObtemNivelTanque(cmbNivelTanque.SelectedItem.ToString());

            locacao.QuilometragemPercorrida = (int)txtQuilometragemPercorrida.Value;

            locacao.DataDevolucaoRealizada = txtDataDevolucao.Value;

            string resultadoValidacao = locacao.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                labelValorTotal.Text = string.Format("{0:C}", locacao.CalcularValorLocacao(configuracao: configuracaoCombustivel));
                btnGravar.Enabled = true;
                TelaPrincipalForm.Instancia.AtualizarRodape("A Devolução está válida e já pode ser concluída");
            }
            else
            {
                btnGravar.Enabled = false;
                StringReader listaDeErros = new StringReader(resultadoValidacao);
                TelaPrincipalForm.Instancia.AtualizarRodape(listaDeErros.ReadLine());
            }
        }

        private static MarcadorCombustivelEnum ObtemNivelTanque(string nivelTanque)
        {
            MarcadorCombustivelEnum marcador = MarcadorCombustivelEnum.Vazio;

            switch (nivelTanque)
            {
                case "Vazio": marcador = MarcadorCombustivelEnum.Vazio; break;
                case "Meio Tanque": marcador = MarcadorCombustivelEnum.MeioTanque; break;
                case "Três Quartos": marcador = MarcadorCombustivelEnum.TresQuartos; break;
                case "Completo": marcador = MarcadorCombustivelEnum.Completo; break;
                default:
                    break;
            }

            return marcador;
        }

        private void listTaxas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var taxa = listTaxas.Items[e.Index] as Taxa;

            if (taxa == null)
                return;

            if (e.NewValue == CheckState.Checked)
                locacao.ConfigurarTaxa(taxa, EstadoTaxaLocacaoEnum.Adicionada);
            else
                locacao.RemoverTaxaLocacao(taxa);

            ConfigurarDevolucao();
        }
    }
}
