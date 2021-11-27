using LocadoraVeiculos.Dominio.CupomModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public partial class TelaCupomForm : Form
    {
        private readonly List<Parceiro> parceiros;
        private Cupom cupom;
        private bool preenchendoCampos = false;

        public TelaCupomForm()
        {
            InitializeComponent();
        }

        public TelaCupomForm(List<Parceiro> parceiros) : this()
        {
            this.parceiros = parceiros;

            preenchendoCampos = true;

            foreach (var item in parceiros)
            {
                cmbParceiros.Items.Add(item);
            }

            preenchendoCampos = false;
        }

        public Cupom Cupom
        {
            get
            {
                return cupom;
            }
            set
            {
                cupom = value;
                preenchendoCampos = true;

                txtId.Text = cupom.Id.ToString();
                txtNome.Text = cupom.Nome;
                txtValor.Text = cupom.Valor.ToString();
                txtValorMinimo.Text = cupom.ValorMinimo.ToString();
                txtDataValidade.Value = cupom.DataValidade;
                cmbParceiros.SelectedItem = cupom.Parceiro;
                cmbTipo.SelectedIndex = Convert.ToInt32(cupom.Tipo);

                preenchendoCampos = false;

                ConfigurarLocacao();
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            ConfigurarLocacao();
        }


        private void ConfigurarLocacao()
        {
            if (preenchendoCampos)
                return;

            cupom.Nome = txtNome.Text;
            cupom.Valor = Convert.ToDecimal(txtValor.Text);
            cupom.ValorMinimo = Convert.ToDecimal(txtValorMinimo.Text);
            cupom.DataValidade = txtDataValidade.Value;
            cupom.Parceiro = cmbParceiros.SelectedItem as Parceiro;

            if (cmbTipo.SelectedIndex == 0)
                cupom.Tipo = TipoCupomEnum.ValorFixo;
            else
                cupom.Tipo = TipoCupomEnum.Percentual;

            string resultadoValidacao = cupom.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                btnGravar.Enabled = true;
                TelaPrincipalForm.Instancia.AtualizarRodape("O cupom está válida e já pode ser cadastrado");
            }
            else
            {
                btnGravar.Enabled = false;
                StringReader listaDeErros = new StringReader(resultadoValidacao);
                TelaPrincipalForm.Instancia.AtualizarRodape(listaDeErros.ReadLine());
            }
        }
    }
}
