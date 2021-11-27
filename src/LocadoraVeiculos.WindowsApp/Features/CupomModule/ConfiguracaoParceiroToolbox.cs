using LocacaoVeiculos.WindowsApp.Shared;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public class ConfiguracaoParceiroToolbox : IConfiguracaoToolbox
    {
        public string ObtemDescricao()
        {
            return "Cadastro de Parceiros";
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
                Adicionar = "Registrar novo Parceiro",
                Editar = "Alterar informações de um Parceiro",
                Excluir = "Excluir um Parceiro"
            };
        }
    }
}
