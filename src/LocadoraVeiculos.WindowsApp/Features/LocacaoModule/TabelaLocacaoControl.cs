using LocacaoVeiculos.WindowsApp.Shared;
using LocadoraVeiculos.Dominio.LocacaoModule;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.LocacaoModule
{
    public partial class TabelaLocacaoControl : UserControl
    {
        public TabelaLocacaoControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Veiculo", HeaderText = "Veículo"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Cliente", HeaderText = "Cliente"},

                new DataGridViewTextBoxColumn { DataPropertyName = "DataAluguel", HeaderText = "Data da Locação"},

                new DataGridViewTextBoxColumn {DataPropertyName = "DataDevolucaoPrevista", HeaderText = "Devolução Prevista "}
            };

            return colunas;
        }

        public int ObterIdSelecionado()
        {
            return grid.SelecionarId<int>();
        }

        internal void AtualizarRegistros(List<Locacao> registros)
        {
            grid.Rows.Clear();

            foreach (var item in registros)
            {
                grid.Rows.Add(item.Id, item.Veiculo.Modelo,
                    item.Condutor.Cliente.Nome, item.DataLocacao, item.DataDevolucaoPrevista);
            }
        }


    }
}
