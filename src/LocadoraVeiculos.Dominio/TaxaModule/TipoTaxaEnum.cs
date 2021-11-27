using System.ComponentModel;

namespace LocadoraVeiculos.Dominio.TaxaModule
{
    public enum TipoTaxaEnum : int
    {
        [Description("Cobrado por dia")]
        CobradoPorDia = 0,

        [Description("Cobrado uma vez")]
        CobradoUmaVez = 1
    }
}