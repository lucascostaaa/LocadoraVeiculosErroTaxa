using LocacaoVeiculos.WindowsApp.Shared;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public class OperacoesCupom : ICadastravel
    {
        private TabelaCupomControl tabelaCupons;

        private readonly ICupomAppService cupomService;
        private readonly IParceiroAppService parceiroService;
        private readonly INotificador notificador;

        public OperacoesCupom(
            ICupomAppService cupomService,
            IParceiroAppService parceiroService,
            INotificador notificador
            )
        {
            this.cupomService = cupomService;
            this.parceiroService = parceiroService;
            this.notificador = notificador;
        }

        public void InserirNovoRegistro()
        {
            var parceiros = parceiroService.SelecionarTodos();

            var tela = new TelaCupomForm(parceiros);
            tela.Cupom = new Cupom();

            if (tela.ShowDialog() == DialogResult.OK)
            {
                Cupom cupom = tela.Cupom;

                bool registroRealizado = cupomService.RegistrarNovoCupom(cupom);

                if(registroRealizado == false)
                {
                    var msgErro = notificador.ObterNotificacoes()[0];
                    TelaPrincipalForm.Instancia.AtualizarRodape(msgErro);
                }
                

                var registros = cupomService.SelecionarTodos();

                tabelaCupons.AtualizarRegistros(registros);

                Log.Logger.Aqui().FuncionalidadeUsada();
            }
        }

        public void EditarRegistro()
        {
            int id = tabelaCupons.ObterIdSelecionado();

            if (id == 0)
            {
                MessageBox.Show("Selecione um cupon para poder editar!", "Edição de Cupons",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var parceiros = parceiroService.SelecionarTodos();

            var tela = new TelaCupomForm(parceiros);

            var cupomSelecionado = cupomService.SelecionarPorId(id);

            tela.Cupom = cupomSelecionado;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                Cupom cupom = tela.Cupom;

                var resultado = cupomService.EditarCupom(cupom.Id, cupom);

                TelaPrincipalForm.Instancia.AtualizarRodape(resultado);

                var registros = cupomService.SelecionarTodos();

                tabelaCupons.AtualizarRegistros(registros);

                Log.Logger.Aqui().FuncionalidadeUsada();
            }
        }

        public void ExcluirRegistro()
        {
            var id = tabelaCupons.ObterIdSelecionado();

            if (id == 0)
            {
                MessageBox.Show("Selecione um cupom para poder exluir!", "Exclusão de Cupons",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var cupomSelecionado = cupomService.SelecionarPorId(id);

            if (MessageBox.Show($"Tem certeza que deseja excluir o cupom: [{cupomSelecionado.Id}] ?", "Exclusão de Cupons",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                cupomService.ExcluirCupom(cupomSelecionado.Id);

                var registros = cupomService.SelecionarTodos();

                tabelaCupons.AtualizarRegistros(registros);

                Log.Logger.Aqui().FuncionalidadeUsada();
            }
        }

        public UserControl ObterTabela()
        {
            if (tabelaCupons == null)
                tabelaCupons = new TabelaCupomControl();

            var registros = cupomService.SelecionarTodos();

            tabelaCupons.AtualizarRegistros(registros);

            return tabelaCupons;
        }
    }
}
