using LocadoraVeiculos.Aplicacao.LocacaoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WindowsServices
{
    public class EnvioEmailWorker : BackgroundService
    {
        private readonly ILogger<EnvioEmailWorker> _logger;
        private readonly ILocacaoAppService locacaoAppService;
        private readonly ILocacaoRepository locacaoRepository;
        private readonly IGeradorRelatorioLocacao geradorRelatorioLocacao;
        private readonly INotificadorEmailLocacao notificadorEmail;

        public EnvioEmailWorker(ILogger<EnvioEmailWorker> logger,
            ILocacaoAppService locacaoAppService,
            ILocacaoRepository locacaoRepository,
            IGeradorRelatorioLocacao geradorRelatorioLocacao,
            INotificadorEmailLocacao notificadorEmail)
        {
            _logger = logger;
            this.locacaoAppService = locacaoAppService;
            this.locacaoRepository = locacaoRepository;
            this.geradorRelatorioLocacao = geradorRelatorioLocacao;
            this.notificadorEmail = notificadorEmail;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //var solicitacoes = locacaoRepository.SelecionarSolicitacoesDeEnvioEmail("Pendentes");

                //if (solicitacoes.Count == 0)
                //{
                //    _logger.LogInformation("Nenhum email para enviar... tentando novamente daqui 5 segundos...");
                //    await Task.Delay(5000, stoppingToken);
                //    continue;
                //}

                //Parallel.ForEach(solicitacoes, (s) =>
                //{
                //    locacaoAppService.EnviarEmails(s);
                //});
            }
        }
    }
}