using FluentResults;
using System;

namespace LocadoraVeiculos.Dominio.LocacaoModule
{
    public class ValidadorLocacao
    {
        public Result Validar(Locacao locacao)
        {
            var resultadoValidacao = Result.Merge(
                Result.FailIf(locacao.Funcionario == null, "Selecione um funcionário"),
                Result.FailIf(locacao.Condutor == null, "Selecione um condutor"),
                Result.FailIf(locacao.Veiculo == null, "Selecione um veículo"),
                Result.FailIf(locacao.Veiculo != null && locacao.Id == 0 && locacao.Veiculo.EstaAlugado(), "O Veículo já está alugado"),
                Result.FailIf(locacao.PlanoCobranca == null, "Selecione o plano de cobrança"),
                Result.FailIf(locacao.DataLocacao == DateTime.MinValue, "Selecione a data da locação"),
                Result.FailIf(locacao.DataDevolucaoPrevista == DateTime.MinValue, "Selecione a data prevista da entrega"),
                Result.FailIf(locacao.DataDevolucaoPrevista < locacao.DataLocacao, "A data prevista da entrega não pode ser menor que data da locação")
            );

            if (locacao.RegistrandoDevolucao)
            {
                Result.Merge(resultadoValidacao,
                    Result.FailIf(locacao.QuilometragemPercorrida == 0, "Informe a quilometragem percorrida"),
                    Result.FailIf(locacao.DataDevolucaoPrevista < locacao.DataLocacao, "A data prevista da entrega não pode ser menor que data da locação"),
                    Result.FailIf(locacao.MarcadorCombustivel == MarcadorCombustivelEnum.NaoInformado,
                    "Informe o status do tanque de combustível")
                );
            }

            return resultadoValidacao;
        }
    }
}
