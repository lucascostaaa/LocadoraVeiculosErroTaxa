using System.ComponentModel;

namespace LocadoraVeiculos.Dominio.GrupoVeiculoModule
{
    public enum TipoPlanoCobrancaEnum : int
    {
        [Description("Diário")]
        PlanoDiario = 1,

        [Description("Km Controlado")]
        PlanoKmControlado = 2,

        [Description("Km Livre")]
        PlanoKmLivre = 3
    }
}
