using LocacaoVeiculos.WindowsApp.Shared;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public class ConfiguracaoCupomToolbox : IConfiguracaoToolbox
    {
        public string ObtemDescricao()
        {
            return "Cadastro de Cupons";
        }

        public ConfiguracaoEstadoBotoes ObtemEstadoBotoes()
        {
            return new ConfiguracaoEstadoBotoes()
            {
                Adicionar = true,
                Editar = true,
                Excluir = true
            };
        }

        public ConfiguracaoTooltips ObtemToolTips()
        {
            return new ConfiguracaoTooltips()
            {
                Adicionar = "Registrar novo Cupom",
                Editar = "Alterar informações de um Cupom",
                Excluir = "Excluir um Cupom"
            };
        }
    }
}
