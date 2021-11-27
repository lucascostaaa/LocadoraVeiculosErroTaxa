using LocacaoVeiculos.WindowsApp.Shared;
using LocadoraVeiculos.Dominio.CupomModule;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public partial class TabelaParceiroControl : UserControl
    {
        public TabelaParceiroControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome"}
            };

            return colunas;
        }

        public int ObterIdSelecionado()
        {
            return grid.SelecionarId<int>();
        }

        internal void AtualizarRegistros(List<Parceiro> registros)
        {
            grid.Rows.Clear();

            foreach (var item in registros)
            {
                grid.Rows.Add(item.Id, item.Nome);
            }
        }
    }
}
