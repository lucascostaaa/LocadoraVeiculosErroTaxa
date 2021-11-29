using System.ComponentModel;

namespace LocadoraVeiculos.Dominio
{
    public enum TipoPessoaEnum
    {
        [Description("Pessoa Fisica")]
        Fisica = 1,

        [Description("Pessoa Juridica")]
        Juridica = 2
    }

}