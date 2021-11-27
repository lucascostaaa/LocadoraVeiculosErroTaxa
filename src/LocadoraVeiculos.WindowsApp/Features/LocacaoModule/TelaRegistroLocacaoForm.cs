using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.LocacaoModule
{
    public partial class TelaRegistroLocacaoForm : Form
    {
        private bool preenchendoCampos;

        private readonly List<Veiculo> veiculos;
        private readonly List<Cupom> cuponsAtivos;
        private Locacao locacao;

        public TelaRegistroLocacaoForm()
        {
            InitializeComponent();
        }

        public TelaRegistroLocacaoForm(
           Funcionario funcionario,
           List<Cliente> clientes,
           List<GrupoVeiculo> gruposVeiculos,
           List<Veiculo> veiculos,
           List<Taxa> taxas,
           List<Cupom> cuponsAtivos) : this()
        {
            locacao = new Locacao();
            this.veiculos = veiculos;
            this.cuponsAtivos = cuponsAtivos;

            PreencherCamposTela(funcionario, clientes, gruposVeiculos, taxas);
        }

        private void PreencherCamposTela(
            Funcionario funcionario,
            List<Cliente> clientes,
            List<GrupoVeiculo> gruposVeiculos,
            List<Taxa> taxas)
        {
            preenchendoCampos = true;

            CarregarClientes(clientes);
            CarregarGruposVeiculos(gruposVeiculos);
            CarregarTaxas(taxas);

            locacao.RegistrarPara(funcionario);

            txtFuncionario.Text = funcionario.Nome;
            labelValorPrevisto.Text = "";

            preenchendoCampos = false;
        }

        public Locacao Locacao
        {
            get { return locacao; }
            set
            {
                locacao = value;
                preenchendoCampos = true;

                txtId.Text = locacao.Id.ToString();
                txtFuncionario.Text = locacao.Funcionario.Nome;
                cmbClientes.SelectedItem = locacao.Condutor.Cliente;
                cmbCondutores.SelectedItem = locacao.Condutor;
                cmbGrupoVeiculos.SelectedItem = locacao.Veiculo.GrupoVeiculo;
                cmbVeiculos.SelectedItem = locacao.Veiculo;
                cmbPlanosCobranca.SelectedItem = locacao.PlanoCobranca;
                txtKmVeiculo.Text = locacao.Veiculo.Quilometragem.ToString();
                txtDataLocacao.Text = locacao.DataLocacao.ToString();
                txtDevolucaoPrevista.Text = locacao.DataDevolucaoPrevista.ToString();

                if (locacao.TemCupom())
                    txtCupom.Text = locacao.Cupom.Nome;

                for (int i = 0; i < listTaxas.Items.Count; i++)
                {
                    var taxa = listTaxas.Items[i] as Taxa;

                    if (locacao.TaxasSelecionadas.Exists(x => x.Id == taxa.Id))
                        listTaxas.SetItemChecked(i, true);
                }

                preenchendoCampos = false;

                ConfigurarLocacao();
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            ConfigurarLocacao();
        }

        private void listTaxas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var taxa = listTaxas.Items[e.Index] as Taxa;
            if (taxa == null)
                return;

            if (e.NewValue == CheckState.Checked)
                locacao.ConfigurarTaxa(taxa, EstadoTaxaLocacaoEnum.Adicionada);
            else
                locacao.ConfigurarTaxa(taxa, EstadoTaxaLocacaoEnum.Removida);

            ConfigurarLocacao();
        }

        private void btnAplicarCupom_Click(object sender, EventArgs e)
        {
            Cupom cupomSelecionado = cuponsAtivos.FirstOrDefault(c => c.Nome == txtCupom.Text);

            if (cupomSelecionado != null)
            {
                locacao.RegistrarCupom(cupomSelecionado);

                ConfigurarLocacao();
            }
            else
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Cupom não encontrado");
            }
        }

        private void ConfigurarLocacao()
        {
            if (preenchendoCampos)
                return;

            locacao.RegistrarPara(cmbCondutores.SelectedItem as Condutor);

            locacao.RegistrarPara(cmbVeiculos.SelectedItem as Veiculo);

            if (locacao.Veiculo != null)
                txtKmVeiculo.Text = locacao.Veiculo.Quilometragem.ToString();

            locacao.RegistrarComPlano(cmbPlanosCobranca.SelectedItem as PlanoCobranca);
            locacao.DataLocacao = txtDataLocacao.Value;
            locacao.DataDevolucaoPrevista = txtDevolucaoPrevista.Value;

            string resultadoValidacao = locacao.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                labelValorPrevisto.Text = string.Format("{0:C}", locacao.CalcularValorLocacao());
                btnGravar.Enabled = true;
                TelaPrincipalForm.Instancia.AtualizarRodape("A Locação está válida e já pode ser realizada");
            }
            else
            {
                btnGravar.Enabled = false;
                StringReader listaDeErros = new StringReader(resultadoValidacao);
                TelaPrincipalForm.Instancia.AtualizarRodape(listaDeErros.ReadLine());
            }
        }

        #region carregamento dos combobox

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cliente cliente = (Cliente)cmbClientes.SelectedItem;

            CarregarCondutores(cliente.Condutores);

            cmbCondutores.SelectedIndex = -1;

            ConfigurarLocacao();
        }

        private void cmbGrupoVeiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrupoVeiculo grupo = (GrupoVeiculo)cmbGrupoVeiculos.SelectedItem;

            CarregarPlanos(grupo.PlanosCobranca);

            CarregarVeiculos(grupo);

            cmbVeiculos.SelectedIndex = -1;
            cmbPlanosCobranca.SelectedIndex = -1;
            txtKmVeiculo.Text = "";

            ConfigurarLocacao();
        }


        private void CarregarTaxas(List<Taxa> taxas)
        {
            foreach (var item in taxas)
            {
                listTaxas.Items.Add(item);
            }
        }

        private void CarregarClientes(List<Cliente> clientes)
        {
            foreach (var item in clientes)
            {
                cmbClientes.Items.Add(item);
            }
        }

        private void CarregarGruposVeiculos(List<GrupoVeiculo> gruposVeiculos)
        {
            foreach (var item in gruposVeiculos)
            {
                cmbGrupoVeiculos.Items.Add(item);
            }
        }

        private void CarregarVeiculos(GrupoVeiculo grupo)
        {
            var veiculosFiltrados = veiculos.Where(v => v.GrupoVeiculo.Equals(grupo)).ToList();

            cmbVeiculos.Items.Clear();

            foreach (var item in veiculosFiltrados)
            {
                cmbVeiculos.Items.Add(item);
            }
        }

        private void CarregarCondutores(ICollection<Condutor> condutores)
        {
            cmbCondutores.Items.Clear();

            foreach (var item in condutores)
            {
                cmbCondutores.Items.Add(item);
            }
        }

        private void CarregarPlanos(List<PlanoCobranca> planosCobranca)
        {
            cmbPlanosCobranca.Items.Clear();

            foreach (var item in planosCobranca)
            {
                cmbPlanosCobranca.Items.Add(item);
            }
        }

        #endregion
    }
}
