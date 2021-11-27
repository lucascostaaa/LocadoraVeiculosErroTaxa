using LocadoraVeiculos.Dominio.CupomModule;
using System;
using System.IO;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public partial class TelaParceiroForm : Form
    {
        private Parceiro parceiro;
        private bool preenchendoCampos;

        public TelaParceiroForm()
        {
            InitializeComponent();
            parceiro = new Parceiro();
        }

        public Parceiro Parceiro
        {
            get
            {
                return parceiro;
            }
            set
            {
                parceiro = value;
                preenchendoCampos = true;

                txtId.Text = parceiro.Id.ToString();
                txtNome.Text = parceiro.Nome;

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

            parceiro.Nome = txtNome.Text;

            string resultadoValidacao = parceiro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                btnGravar.Enabled = true;
                TelaPrincipalForm.Instancia.AtualizarRodape("O parceiro está válido e já pode ser cadastrado");
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
