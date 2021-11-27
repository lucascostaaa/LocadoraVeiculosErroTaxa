using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.GrupoVeiculoModule
{

    public class PlanoCobranca : EntidadeBase<int>, IEquatable<PlanoCobranca>
    {
        public PlanoCobranca()
        {
        }

        private PlanoCobranca(decimal valorDia, decimal valorKMRodado)
        {
            TipoPlano = TipoPlanoCobrancaEnum.PlanoDiario;
            ValorDia = valorDia;
            ValorKMRodado = valorKMRodado;
        }

        private PlanoCobranca(decimal valorDia, int kilometragemLivreInclusa, decimal valorKMRodado)
        {
            TipoPlano = TipoPlanoCobrancaEnum.PlanoKmControlado;
            ValorDia = valorDia;
            KilometragemLivreInclusa = kilometragemLivreInclusa;
            ValorKMRodado = valorKMRodado;
        }

        private PlanoCobranca(decimal valorDia)
        {
            TipoPlano = TipoPlanoCobrancaEnum.PlanoKmLivre;
            ValorDia = valorDia;
        }

        public static PlanoCobranca Diario(decimal valorDia, decimal valorKMRodado)
        {
            return new PlanoCobranca(valorDia, valorKMRodado);
        }

        public static PlanoCobranca KmControlado(decimal valorDia, int kilometragemLivreInclusa, decimal valorKMRodado)
        {
            return new PlanoCobranca(valorDia, kilometragemLivreInclusa, valorKMRodado);
        }

        public static PlanoCobranca KmLivre(decimal valorDia)
        {
            return new PlanoCobranca(valorDia);
        }

        public decimal ValorDia { get; private set; }

        public int KilometragemLivreInclusa { get; private set; }

        public decimal ValorKMRodado { get; private set; }

        public TipoPlanoCobrancaEnum TipoPlano { get; private set; }

        public GrupoVeiculo GrupoVeiculo { get; set; }

        public int GrupoVeiculoId { get; set; }

        public decimal CalcularValor(int quantidadeDias, int quilometragemPercorrida)
        {
            decimal valorPlano = quantidadeDias * ValorDia;

            if (quilometragemPercorrida > 0)
            {
                if (TipoPlano == TipoPlanoCobrancaEnum.PlanoDiario)
                    valorPlano += quilometragemPercorrida * ValorKMRodado;

                else if (TipoPlano == TipoPlanoCobrancaEnum.PlanoKmControlado && quilometragemPercorrida > KilometragemLivreInclusa)
                {
                    int diferencaQuilometragemRodada = quilometragemPercorrida - KilometragemLivreInclusa;
                    valorPlano += diferencaQuilometragemRodada * ValorKMRodado;
                }
            }

            return valorPlano;
        }

        public override string Validar()
        {
            return "ESTA_VALIDO";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlanoCobranca);
        }

        public bool Equals(PlanoCobranca other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   ValorDia.Equals(other.ValorDia) &&
                   KilometragemLivreInclusa.Equals(other.KilometragemLivreInclusa) &&
                   ValorKMRodado.Equals(other.ValorKMRodado) &&
                   TipoPlano.Equals(other.TipoPlano) &&
                   GrupoVeiculo.Id.Equals(other.GrupoVeiculo.Id);
        }

        public override int GetHashCode()
        {
            int hashCode = 140172107;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + ValorDia.GetHashCode();
            hashCode = hashCode * -1521134295 + KilometragemLivreInclusa.GetHashCode();
            hashCode = hashCode * -1521134295 + ValorKMRodado.GetHashCode();
            hashCode = hashCode * -1521134295 + TipoPlano.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<GrupoVeiculo>.Default.GetHashCode(GrupoVeiculo);
            return hashCode;
        }

        public override string ToString()
        {
            return TipoPlano.GetDescription();
        }
    }
}
