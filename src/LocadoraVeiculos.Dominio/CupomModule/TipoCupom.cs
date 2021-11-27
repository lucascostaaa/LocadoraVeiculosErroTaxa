using System.ComponentModel;

namespace LocadoraVeiculos.Dominio.CupomModule
{
    public enum TipoCupomEnum : int
    {
        [Description("Valor Fixo")]
        ValorFixo = 0,

        [Description("Percentual")]
        Percentual = 1
    }
}