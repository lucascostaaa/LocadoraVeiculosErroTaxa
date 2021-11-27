using LocadoraVeiculos.Dominio.LocacaoModule;
using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.TaxaModule
{
    public class Taxa : EntidadeBase<int>, IEquatable<Taxa>
    {
        public string Nome { get; }
        public decimal Valor { get; }
        public virtual TipoTaxaEnum TipoTaxa { get; set; }

        public virtual EstadoTaxaLocacaoEnum EstadoTaxaLocacao { get; set; }
        public virtual List<Locacao> Locacoes { get; set; }

        public Taxa()
        {
            Locacoes = new List<Locacao>();
        }

        public Taxa(string nome, decimal valor, TipoTaxaEnum tipoTaxa) : this()
        {
            Nome = nome;
            Valor = valor;
            TipoTaxa = tipoTaxa;
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(Nome))
                resultadoValidacao += "O campo nome é obrigatório e não pode ser vazio.";

            if (Valor < 0)
                resultadoValidacao += "Taxa Fixa não pode ser menor que Zero.";

            if (TipoTaxa < 0)
                resultadoValidacao += "Taxa Diaria não pode ser Menor que Zero.";

            if (Valor == 0 && TipoTaxa <= 0)
                resultadoValidacao += "Taxa Diaria não pode ser Menor que Zero.";

            if (TipoTaxa == 0 && Valor <= 0)
                resultadoValidacao += "Taxa Diaria não pode ser Menor que Zero.";

            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }

        public decimal CalcularValor(int quantidadeDias)
        {
            if (TipoTaxa == TipoTaxaEnum.CobradoPorDia)
                return Valor * quantidadeDias;

            return Valor;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Taxa);
        }

        public bool Equals(Taxa other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Nome.Equals(other.Nome) &&
                   Valor.Equals(other.Valor) &&
                   TipoTaxa.Equals(other.TipoTaxa);
        }

        public override int GetHashCode()
        {
            int hashCode = 24973818;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + Valor.GetHashCode();
            hashCode = hashCode * -1521134295 + TipoTaxa.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return Nome;
        }

        internal void AdicionarLocacao(Locacao locacao)
        {
            Locacoes.Add(locacao);
        }
    }
}