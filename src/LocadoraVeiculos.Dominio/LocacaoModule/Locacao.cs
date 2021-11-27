using FluentResults;
using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.ConfiguraoModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Dominio.LocacaoModule
{
    public class Locacao : EntidadeBase<int>, IEquatable<Locacao>
    {
        private const decimal DezPorcento = (10m / 100m);

        public Locacao()
        {
            TaxasSelecionadas = new List<Taxa>();
            EmAberto = true;
        }

        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public DateTime DataDevolucaoRealizada { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public int VeiculoId { get; set; }

        public Veiculo Veiculo { get; set; }

        public int CondutorId { get; set; }
        public virtual Condutor Condutor { get; set; }

        public int PlanoCobrancaId { get; set; }
        public PlanoCobranca PlanoCobranca { get; set; }

        public MarcadorCombustivelEnum MarcadorCombustivel { get; set; }

        public int? CupomId { get; set; }

        public virtual Cupom Cupom { get; set; }

        public SituacaoEnvioEmailEnum SituacaoEnvioEmail { get; set; }


        public bool EmAberto { get; set; }
        public int QuilometragemPercorrida { get; set; }

        public byte[] Relatorio { get; set; }

        public bool RegistrandoDevolucao { get; set; }

        public bool RelatorioAnexado
        {
            get
            {
                return Relatorio != null;
            }
        }


        private int QuantidadeDeDias
        {
            get
            {
                int qtdDiasLocacao;

                if (DataDevolucaoRealizada == DateTime.MinValue)
                    qtdDiasLocacao = (DataDevolucaoPrevista.Date - DataLocacao.Date).Days;
                else
                    qtdDiasLocacao = (DataDevolucaoRealizada - DataLocacao).Days;

                return qtdDiasLocacao;
            }
        }


        public List<Taxa> TaxasSelecionadas { get; set; }

        public void AnexarRelatorio(byte[] pdf)
        {
            Relatorio = pdf;
        }

        public decimal CalcularValorLocacao(ConfiguracaoCombustivel configuracao = null)
        {
            if (PlanoCobranca == null)
                return 0;

            decimal valorPlano = PlanoCobranca.CalcularValor(QuantidadeDeDias, QuilometragemPercorrida);

            decimal valorTaxas = 0;

            if (TaxasSelecionadas != null && TaxasSelecionadas.Any())
                valorTaxas = TaxasSelecionadas.Sum(tx => tx.CalcularValor(QuantidadeDeDias));

            decimal valorCombustivel = 0;

            if (Veiculo != null)
            {
                decimal precoCombustivel = 0;

                if (configuracao != null)
                    precoCombustivel = configuracao.ObtemPrecoCombustivel(Veiculo.TipoCombustivel);

                valorCombustivel = Veiculo.QuantidadeDeListrosParaAbastecer(MarcadorCombustivel) * precoCombustivel;
            }
            decimal valorTotal = valorPlano + valorCombustivel + valorTaxas;

            if (TemMulta())
                valorTotal += valorTotal * DezPorcento;

            if (TemCupom())
                valorTotal -= Cupom.CalcularDesconto(valorTotal);

            return valorTotal;
        }



        public void AlugarVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
                return;

            Veiculo.RegistrarLocacao(this);
        }

        public void RegistrarPara(Veiculo veiculo)
        {
            if (veiculo == null)
                return;

            Veiculo = veiculo;

            VeiculoId = veiculo.Id;
        }

        public void RegistrarPara(Funcionario funcionario)
        {
            if (funcionario == null)
                return;

            FuncionarioId = funcionario.Id;
            Funcionario = funcionario;
        }

        public void RegistrarPara(Condutor condutor)
        {
            if (condutor == null)
                return;

            CondutorId = condutor.Id;
            Condutor = condutor;
        }

        public void RegistrarComPlano(PlanoCobranca plano)
        {
            if (plano == null)
                return;

            PlanoCobrancaId = plano.Id;
            PlanoCobranca = plano;
        }

        public void RegistrarCupom(Cupom cupom)
        {
            if (cupom == null)
                return;

            CupomId = cupom.Id;
            Cupom = cupom;
        }

        public bool TemCupom()
        {
            return Cupom != null;
        }

        public bool TemMulta()
        {
            return (DataDevolucaoRealizada - DataDevolucaoPrevista).Days > 0;
        }

        public void RegistrarDevolucao()
        {
            EmAberto = false;
        }

        public Result Validar2()
        {
            var resultadoValidacao = Result.Merge(
                Result.FailIf(Funcionario == null, "Selecione um funcionário"),
                Result.FailIf(Condutor == null, "Selecione um condutor"),
                Result.FailIf(Veiculo == null, "Selecione um veículo"),
                Result.FailIf(Veiculo != null && Id == 0 && Veiculo.EstaAlugado(), "O Veículo já está alugado"),
                Result.FailIf(PlanoCobranca == null, "Selecione o plano de cobrança"),
                Result.FailIf(DataLocacao == DateTime.MinValue, "Selecione a data da locação"),
                Result.FailIf(DataDevolucaoPrevista == DateTime.MinValue, "Selecione a data prevista da entrega"),
                Result.FailIf(DataDevolucaoPrevista < DataLocacao, "A data prevista da entrega não pode ser menor que data da locação")
            );

            if (RegistrandoDevolucao)
            {
                Result.Merge(resultadoValidacao,
                    Result.FailIf(QuilometragemPercorrida == 0, "Informe a quilometragem percorrida"),
                    Result.FailIf(DataDevolucaoPrevista < DataLocacao, "A data prevista da entrega não pode ser menor que data da locação"),
                    Result.FailIf(MarcadorCombustivel == MarcadorCombustivelEnum.NaoInformado,
                    "Informe o status do tanque de combustível")
                );
            }

            return resultadoValidacao;
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (Funcionario == null)
                resultadoValidacao = "Selecione um funcionário";

            if (Condutor == null)
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "Selecione um condutor";

            if (Veiculo == null)
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "Selecione um veículo";

            if (Veiculo != null && Id == 0 && Veiculo.EstaAlugado())
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "O Veículo já está alugado";

            if (PlanoCobranca == null)
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "Selecione o plano de cobrança";

            if (DataLocacao == DateTime.MinValue)
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "Selecione a data da locação";

            if (DataDevolucaoPrevista == DateTime.MinValue)
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "Selecione a data prevista da entrega";

            if (DataDevolucaoPrevista < DataLocacao)
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "A data prevista da entrega não pode ser menor que data da locação";

            if (RegistrandoDevolucao)
            {
                if (QuilometragemPercorrida == 0)
                    resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "Informe a quilometragem percorrida";

                if (DataDevolucaoPrevista < DataLocacao)
                    resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "A data prevista da entrega não pode ser menor que data da locação";

                if (MarcadorCombustivel == MarcadorCombustivelEnum.NaoInformado)
                    resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "Informe o status do tanque de combustível";
            }

            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }

        public void SelecionarTaxa(Taxa taxa)
        {
            taxa.EstadoTaxaLocacao = EstadoTaxaLocacaoEnum.Gravada;

            TaxasSelecionadas.Add(taxa);
        }

        public void ConfigurarTaxa(Taxa taxa, EstadoTaxaLocacaoEnum estadoTaxa)
        {
            if (estadoTaxa == EstadoTaxaLocacaoEnum.Adicionada)
            {
                if (TaxasSelecionadas.Exists(t => t.Equals(taxa)))
                    return;

                taxa.EstadoTaxaLocacao = EstadoTaxaLocacaoEnum.Adicionada;

                TaxasSelecionadas.Add(taxa);
            }

            else if (estadoTaxa == EstadoTaxaLocacaoEnum.Removida)
            {
                var taxaSelecionda = TaxasSelecionadas.FirstOrDefault(x => x.Equals(taxa));

                if (taxaSelecionda != null)
                    taxaSelecionda.EstadoTaxaLocacao = EstadoTaxaLocacaoEnum.Removida;
            }
        }

        public void RemoverTaxaLocacao(Taxa taxa)
        {
            TaxasSelecionadas.Remove(taxa);
        }

        public List<Taxa> TaxasAdicionadas()
        {
            return TaxasSelecionadas
                .Where(x => x.EstadoTaxaLocacao == EstadoTaxaLocacaoEnum.Adicionada)
                .ToList();
        }

        public List<Taxa> TaxasRemovidas()
        {
            return TaxasSelecionadas
                .Where(x => x.EstadoTaxaLocacao == EstadoTaxaLocacaoEnum.Removida)
                .ToList();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Locacao);
        }

        public bool Equals(Locacao other)
        {
            return other != null &&
                   Id == other.Id &&
                   EqualsTaxasSelecionadas(other.TaxasSelecionadas) &&
                   DataLocacao.Equals(other.DataLocacao) &&
                   DataDevolucaoPrevista.Equals(other.DataDevolucaoPrevista) &&
                   DataDevolucaoRealizada.Equals(other.DataDevolucaoRealizada) &&
                   Funcionario.Equals(other.Funcionario) &&
                   Veiculo.Equals(other.Veiculo) &&
                   Condutor.Equals(other.Condutor) &&
                   PlanoCobranca.Equals(other.PlanoCobranca) &&
                   EmAberto.Equals(other.EmAberto) &&
                   QuilometragemPercorrida.Equals(other.QuilometragemPercorrida) &&
                   MarcadorCombustivel.Equals(other.MarcadorCombustivel) &&
                   CuponsSaoIguais(other) &&
                   QuantidadeDeDias.Equals(other.QuantidadeDeDias);
        }

        private bool CuponsSaoIguais(Locacao other)
        {
            var cuponsIguais = false;

            if (TemCupom())
                cuponsIguais = Cupom.Equals(other.Cupom);

            else if (Cupom == null && other.Cupom == null)
                cuponsIguais = true;

            return cuponsIguais;
        }

        public bool EqualsTaxasSelecionadas(List<Taxa> outrasTaxasSelecionadas)
        {
            return (TaxasSelecionadas.All(item => outrasTaxasSelecionadas.Contains(item)) &&
                    TaxasSelecionadas.Distinct().Count() == TaxasSelecionadas.Count &&
                    TaxasSelecionadas.Count == outrasTaxasSelecionadas.Count);
        }

        public override int GetHashCode()
        {
            int hashCode = 64544170;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Taxa>>.Default.GetHashCode(TaxasSelecionadas);
            hashCode = hashCode * -1521134295 + DataLocacao.GetHashCode();
            hashCode = hashCode * -1521134295 + DataDevolucaoPrevista.GetHashCode();
            hashCode = hashCode * -1521134295 + DataDevolucaoRealizada.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Funcionario>.Default.GetHashCode(Funcionario);
            hashCode = hashCode * -1521134295 + EqualityComparer<Veiculo>.Default.GetHashCode(Veiculo);
            hashCode = hashCode * -1521134295 + EqualityComparer<Condutor>.Default.GetHashCode(Condutor);
            hashCode = hashCode * -1521134295 + EqualityComparer<PlanoCobranca>.Default.GetHashCode(PlanoCobranca);
            hashCode = hashCode * -1521134295 + EmAberto.GetHashCode();
            hashCode = hashCode * -1521134295 + QuilometragemPercorrida.GetHashCode();
            hashCode = hashCode * -1521134295 + MarcadorCombustivel.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Cupom>.Default.GetHashCode(Cupom);
            hashCode = hashCode * -1521134295 + QuantidadeDeDias.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, DataLocacao: {1}, Funcionario: {2}, Veiculo: {3}, Condutor: {4}, PlanoSelecionado: {5}",
                   Id, DataLocacao, Funcionario, Veiculo, Condutor, PlanoCobranca);
        }
    }
}
