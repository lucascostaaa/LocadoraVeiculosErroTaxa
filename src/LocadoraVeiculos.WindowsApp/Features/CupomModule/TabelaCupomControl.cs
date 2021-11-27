using LocacaoVeiculos.WindowsApp.Shared;
using LocadoraVeiculos.Dominio.CupomModule;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public partial class TabelaCupomControl : UserControl
    {
        public TabelaCupomControl()
        {
            InitializeComponent();

            grid.ConfigurarGridSomenteLeitura();
            grid.ConfigurarGridZebrado();
            grid.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Valor", HeaderText = "Valor"},

                new DataGridViewTextBoxColumn { DataPropertyName = "DataValidade", HeaderText = "Data de Validade"},

                new DataGridViewTextBoxColumn {DataPropertyName = "Parceiro", HeaderText = "Parceiro"}
            };

            return colunas;
        }

        public int ObterIdSelecionado()
        {
            return grid.SelecionarId<int>();
        }

        internal void AtualizarRegistros(List<Cupom> registros)
        {
            grid.Rows.Clear();

            foreach (var item in registros)
            {
                grid.Rows.Add(item.Id, item.Nome,
                    item.Valor, item.DataValidade, item.Parceiro.Nome);
            }
        }
    }
}
