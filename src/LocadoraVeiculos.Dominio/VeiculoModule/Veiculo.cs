using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Dominio.VeiculoModule
{

    public class Veiculo : EntidadeBase<int>, IEquatable<Veiculo>
    {
        public Veiculo()
        {
        }

        public Veiculo(string placa, string modelo, string fabricante, double quilometragem, int qtdlitros,
            TipoCombustivelEnum combustivel, GrupoVeiculo grupo)
        {
            Placa = placa;
            Modelo = modelo;
            Fabricante = fabricante;
            Quilometragem = quilometragem;
            QtdLitrosTanque = qtdlitros;
            TipoCombustivel = combustivel;
            GrupoVeiculo = grupo;
            Locacoes = new List<Locacao>();
        }

        public string Placa { get; private set; }
        public TipoCombustivelEnum TipoCombustivel { get; private set; }
        public string Modelo { get; private set; }
        public string Fabricante { get; private set; }
        public double Quilometragem { get; private set; }
        public int QtdLitrosTanque { get; private set; }
        public int QtdPortas { get; set; }
        public string NumeroChassi { get; set; }
        public string Cor { get; set; }
        public int CapacidadeOcupantes { get; set; }
        public int AnoFabricacao { get; set; }
        public string TamanhoPortaMalas { get; set; }
        public byte[] Imagem { get; set; }

        public List<Locacao> Locacoes { get; private set; }
        public int GrupoVeiculoId { get; private set; }

        public GrupoVeiculo GrupoVeiculo { get; private set; }

        public override string ToString()
        {
            return $"{Modelo} -> {Placa}";
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(Placa))
                resultadoValidacao = "O campo Placa é obrigatório";

            if (string.IsNullOrEmpty(Modelo))
                resultadoValidacao = "O campo Modelo é obrigatório";

            if (string.IsNullOrEmpty(Fabricante))
                resultadoValidacao = "O campo Fabricante é obrigatório";

            if (Quilometragem < 0)
                resultadoValidacao = "O campo Quilometragem não pode ser menor que zero";

            if (QtdLitrosTanque <= 0)
                resultadoValidacao = "O campo do Tanque de Combustivel não pode ser menor que zero";

            if (string.IsNullOrEmpty(TipoCombustivel.ToString()))
                resultadoValidacao = "O campo Tipo de combustivel é obrigatório";

            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }

        public int QuantidadeDeListrosParaAbastecer(MarcadorCombustivelEnum marcadorCombustivel)
        {
            switch (marcadorCombustivel)
            {
                case MarcadorCombustivelEnum.Vazio: return QtdLitrosTanque;

                case MarcadorCombustivelEnum.UmQuarto: return (QtdLitrosTanque - (QtdLitrosTanque * 1 / 4));

                case MarcadorCombustivelEnum.MeioTanque: return (QtdLitrosTanque - (QtdLitrosTanque * 1 / 2));

                case MarcadorCombustivelEnum.TresQuartos: return (QtdLitrosTanque - (QtdLitrosTanque * 3 / 4));

                default:
                    return 0;
            }
        }

        public void RegistrarLocacao(Locacao locacao)
        {
            if (Locacoes.Exists(x => x.Id == locacao.Id) == false)
                Locacoes.Add(locacao);
        }

        public void RegistrarLocacoes(List<Locacao> locacoes)
        {
            foreach (var item in locacoes)
            {
                RegistrarLocacao(item);
            }
        }

        public bool EstaAlugado()
        {
            return Locacoes.Count(x => x.EmAberto) > 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Veiculo);
        }

        public bool Equals(Veiculo other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Placa.Equals(other.Placa) &&
                   TipoCombustivel.Equals(other.TipoCombustivel) &&
                   Modelo.Equals(other.Modelo) &&
                   Fabricante.Equals(other.Fabricante) &&
                   Quilometragem.Equals(other.Quilometragem) &&
                   QtdLitrosTanque.Equals(other.QtdLitrosTanque) &&
                   GrupoVeiculo.Equals(other.GrupoVeiculo) &&
                   Imagem.SequenceEqual(other.Imagem);
        }



        public override int GetHashCode()
        {
            int hashCode = -1847936963;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Placa);
            hashCode = hashCode * -1521134295 + TipoCombustivel.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Modelo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Fabricante);
            hashCode = hashCode * -1521134295 + Quilometragem.GetHashCode();
            hashCode = hashCode * -1521134295 + QtdLitrosTanque.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<GrupoVeiculo>.Default.GetHashCode(GrupoVeiculo);
            hashCode = hashCode * -1521134295 + QtdPortas.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NumeroChassi);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cor);
            hashCode = hashCode * -1521134295 + CapacidadeOcupantes.GetHashCode();
            hashCode = hashCode * -1521134295 + AnoFabricacao.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TamanhoPortaMalas);
            return hashCode;
        }

        public void AtualizarQuilometragem(int quilometragemPercorrida)
        {
            Quilometragem += quilometragemPercorrida;
        }
    }
}
