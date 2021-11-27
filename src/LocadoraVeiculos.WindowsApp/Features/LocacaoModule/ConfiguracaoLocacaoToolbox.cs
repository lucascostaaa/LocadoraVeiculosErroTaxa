using LocacaoVeiculos.WindowsApp.Shared;

namespace LocadoraVeiculos.WindowsApp.Features.LocacaoModule
{
    public class ConfiguracaoLocacaoToolbox : IConfiguracaoToolbox
    {
        public string ObtemDescricao()
        {
            return "Controle de Locações";
        }

        public ConfiguracaoEstadoBotoes ObtemEstadoBotoes()
        {
            return new ConfiguracaoEstadoBotoes()
            {
                Adicionar = true,
                Concluir = true,
                Editar = true,
                Excluir = true,
                Enviar = true
            };
        }

        public ConfiguracaoTooltips ObtemToolTips()
        {
            return new ConfiguracaoTooltips()
            {
                Adicionar = "Registrar a Locação de um Veículo",
                Concluir = "Registrar a devolução de um Veículo",
                Editar = "Alterar informações de uma Locação",
                Excluir = "Excluir uma Locação",
                Enviar = "Reenviar email da fatura da Locação"
            };
        }
    }
}
