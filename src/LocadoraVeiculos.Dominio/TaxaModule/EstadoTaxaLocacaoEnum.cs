using System.ComponentModel;

namespace LocadoraVeiculos.Dominio.TaxaModule
{
    public enum EstadoTaxaLocacaoEnum
    {
        [Description("Adicionada")]
        Adicionada = 0,

        [Description("Removida")]
        Removida = 1,

        [Description("Gravada")]
        Gravada = 2
    }
}