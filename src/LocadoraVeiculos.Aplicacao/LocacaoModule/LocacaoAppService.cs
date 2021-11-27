using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.LocacaoModule
{
    public interface ILocacaoAppService
    {
        string RegistrarNovaLocacao(Locacao locacao);

        string RegistrarDevolucao(Locacao locacao);

        string EditarLocacao(Locacao locacao);

        string ExcluirLocacao(Locacao locacao);

        string ExcluirLocacao(int id);

        Locacao SelecionarPorId(int id);

        List<Locacao> SelecionarTodos();

        //void EnviarEmails(object solicitacoes);
    }

    public class LocacaoAppService : ILocacaoAppService
    {
        #region mensagens
        private const string IdLocacaoFormat = "[Id da Locação: {LocacaoId}]";

        private const string LocacaoNaoRegistrada_ProblemasComBanco =
            "Locação NÃO registrada. Tivemos problemas com a inserção no banco de dados ";

        private const string LocacaoRegistrada_MasProblemasComRelatorio =
            "Locação registrada, mas tivemos problemas na geração do Relatório PDF ";

        private const string LocacaoRegistrada_EmailEnviado =
            "Locação registrada e o Relatório PDF foi enviado por e-mail ";

        private const string LocacaoRegistrada_RelatorioNaoEnviado =
            "Locação registrada, mas o Relatório PDF NÃO foi enviado por e-mail ";

        private const string DevolucaoNaoRegistrada_ProblemasComBanco =
            "Devolução NÃO registrada. Tivemos problemas com a atualização no banco de dados";

        private const string DevolucaoRegistrada_ProblemasAtualizacaoKmVeiculo =
            "Devolução registrada. Mas tivemos problemas na atualização da quilometragem do veículo...";

        private const string DevolucaoRegistrada_ComSucesso =
            "Devolução do veículo registrada com sucesso";

        private const string LocacaoAtualizada_ComSucesso =
            "Locação atualizada com sucesso";

        private const string LocacaoNaoAtualizada_ProblemasComBanco =
            "Locação não atualizada. Tivemos problemas com a atualização no banco de dados";

        private const string LocacaoNaoExcluida_ProblemasComBanco =
            "Locação não excluída. Tivemos problemas com a exclusão no banco de dados";

        private const string LocacaoExcluida_ComSucesso =
            "Locação excluída com sucesso";

        private const string LocacaoNaoEncontrada =
            "Locação não encontrada";

        #endregion

        private readonly ILocacaoRepository locacaoRepository;
        private readonly IGeradorRelatorioLocacao geradorRelatorio;
        private readonly IVerificadorConexaoInternet verificadorInternet;
        private readonly INotificadorEmailLocacao notificadorEmail;
        private readonly IVeiculoRepository veiculoRepository;

        public LocacaoAppService(ILocacaoRepository locacaoRepository,
           IGeradorRelatorioLocacao geradorRelatorio,
           IVerificadorConexaoInternet verificadorInternet,
           INotificadorEmailLocacao notificadorEmail,
           IVeiculoRepository veiculoRepository)
        {
            this.locacaoRepository = locacaoRepository;
            this.geradorRelatorio = geradorRelatorio;
            this.verificadorInternet = verificadorInternet;
            this.notificadorEmail = notificadorEmail;
            this.veiculoRepository = veiculoRepository;
        }

        public string RegistrarNovaLocacao(Locacao locacao)
        {
            var resultado = locacao.Validar();

            if (resultado != "ESTA_VALIDO")
                return resultado;

            var locacaoInserida = locacaoRepository.Inserir(locacao);

            if (locacaoInserida == false)
            {
                Log.Logger.Aqui().Warning(LocacaoNaoRegistrada_ProblemasComBanco + IdLocacaoFormat, locacao.Id);

                return LocacaoNaoRegistrada_ProblemasComBanco;
            }

            var pdf = geradorRelatorio.GerarRelatorioPdf(locacao);

            if (pdf != null)
                locacao.AnexarRelatorio(pdf);
            else
            {
                locacao.SituacaoEnvioEmail = SituacaoEnvioEmailEnum.EmailPendente;

                locacaoRepository.Editar(locacao);

                Log.Logger.Aqui().Warning(LocacaoRegistrada_MasProblemasComRelatorio + IdLocacaoFormat, locacao.Id);

                return LocacaoRegistrada_MasProblemasComRelatorio;
            }

            bool acessoInternet = verificadorInternet.TemConexaoComInternet();

            bool emailEnviado = false;

            if (acessoInternet)
                emailEnviado = notificadorEmail.EnviarEmailLocacao(locacao);

            if (emailEnviado)
            {
                locacao.SituacaoEnvioEmail = SituacaoEnvioEmailEnum.EmailEnviado;

                resultado = LocacaoRegistrada_EmailEnviado;

                Log.Logger.Aqui().Information(LocacaoRegistrada_EmailEnviado + IdLocacaoFormat, locacao.Id);
            }
            else
            {
                locacao.SituacaoEnvioEmail = SituacaoEnvioEmailEnum.EmailPendente;

                resultado = LocacaoRegistrada_EmailEnviado;

                Log.Logger.Aqui().Warning(LocacaoRegistrada_RelatorioNaoEnviado + IdLocacaoFormat, locacao.Id);
            }

            locacaoRepository.Editar(locacao);

            return resultado;
        }

        public string RegistrarDevolucao(Locacao locacao)
        {
            locacao.RegistrarDevolucao();

            var locacaoAtualizada = locacaoRepository.Editar(locacao.Id, locacao);

            if (locacaoAtualizada == false)
            {
                Log.Logger.Aqui().Information(DevolucaoNaoRegistrada_ProblemasComBanco + IdLocacaoFormat, locacao.Id);

                return DevolucaoNaoRegistrada_ProblemasComBanco;
            }

            var veiculo = locacao.Veiculo;

            veiculo.AtualizarQuilometragem(locacao.QuilometragemPercorrida);

            var veiculoAtualizado = veiculoRepository.Editar(veiculo.Id, veiculo);

            if (veiculoAtualizado == false)
            {
                Log.Logger.Aqui().Information(DevolucaoRegistrada_ProblemasAtualizacaoKmVeiculo + IdLocacaoFormat, locacao.Id);

                return DevolucaoRegistrada_ProblemasAtualizacaoKmVeiculo;
            }

            return DevolucaoRegistrada_ComSucesso;
        }

        public string EditarLocacao(Locacao locacao)
        {
            var locacaoAtualizada = locacaoRepository.Editar(locacao.Id, locacao);

            if (locacaoAtualizada == false)
            {
                Log.Logger.Aqui().Information(LocacaoNaoAtualizada_ProblemasComBanco + IdLocacaoFormat, locacao.Id);

                return LocacaoNaoAtualizada_ProblemasComBanco;
            }

            return LocacaoAtualizada_ComSucesso;
        }

        public string ExcluirLocacao(int id)
        {
            var locacaoExcluida = locacaoRepository.Excluir(id);

            if (locacaoExcluida == false)
            {
                Log.Logger.Aqui().Information(LocacaoNaoExcluida_ProblemasComBanco + IdLocacaoFormat, id);

                return LocacaoNaoExcluida_ProblemasComBanco;
            }

            return LocacaoExcluida_ComSucesso;
        }

        public string ExcluirLocacao(Locacao locacao)
        {
            return ExcluirLocacao(locacao.Id);
        }

        public Locacao SelecionarPorId(int id)
        {
            var locacao = locacaoRepository.SelecionarPorId(id);

            if (locacao == null)
            {
                Log.Logger.Aqui().Information(LocacaoNaoEncontrada + IdLocacaoFormat, id);
            }

            return locacao;
        }

        public List<Locacao> SelecionarTodos()
        {
            return locacaoRepository.SelecionarTodos();
        }

        public void EnviarEmails(object solicitacao)
        {
            //var locacao = solicitacao.Locacao;

            //var pdf = geradorRelatorioLocacao.GerarRelatorioPdf(locacao);

            //var resultado = notificadorEmail.EnviarEmailLocacao(locacao);

            //var status = resultado == "Email_enviado" ? Status.EmailEnviado : Status.EmailPendente;

            //solicitacoesRepository.AtualizarStatusSolicitacao(status);
        }

    }
}
