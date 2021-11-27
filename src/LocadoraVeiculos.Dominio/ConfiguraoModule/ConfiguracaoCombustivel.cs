using LocadoraVeiculos.Dominio.VeiculoModule;

namespace LocadoraVeiculos.Dominio.ConfiguraoModule
{
    public class ConfiguracaoCombustivel
    {
        public decimal ValorGasolina { get; set; }

        public decimal ValorDiesel { get; set; }

        public decimal ValorAlcool { get; set; }

        public decimal ObtemPrecoCombustivel(TipoCombustivelEnum tipoCombustivel)
        {
            switch (tipoCombustivel)
            {
                case TipoCombustivelEnum.Gasolina: return ValorGasolina;

                case TipoCombustivelEnum.Alcool: return ValorAlcool;

                case TipoCombustivelEnum.Diesel: return ValorDiesel;

                default: return 0;
            }
        }
    }
}
