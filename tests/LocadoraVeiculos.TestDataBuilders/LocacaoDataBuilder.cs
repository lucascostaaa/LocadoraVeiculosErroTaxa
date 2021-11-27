using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using System;
using System.IO;

namespace LocadoraVeiculos.TestDataBuilders
{
    public class LocacaoDataBuilder
    {
        private Locacao locacao;

        public LocacaoDataBuilder()
        {
            locacao = new Locacao();
        }

        public LocacaoDataBuilder ComPlanoDeCobranca(PlanoCobranca planoCobranca)
        {
            locacao.RegistrarComPlano(planoCobranca);
            return this;
        }

        public LocacaoDataBuilder NaData(DateTime data)
        {
            locacao.DataLocacao = data;
            return this;
        }

        public LocacaoDataBuilder ComDataDeDevolucaoPrevista(DateTime data)
        {
            locacao.DataDevolucaoPrevista = data;
            return this;
        }

        public LocacaoDataBuilder ComQuilometragemPercorrida(int quilometragemPercorrida)
        {
            locacao.QuilometragemPercorrida = quilometragemPercorrida;
            return this;
        }

        public LocacaoDataBuilder ConfigurarTaxa(Taxa taxa, EstadoTaxaLocacaoEnum estadoTaxa)
        {
            locacao.ConfigurarTaxa(taxa, estadoTaxa);

            return this;
        }

        public LocacaoDataBuilder ComCupom(Cupom cupom)
        {
            locacao.RegistrarCupom(cupom);
            return this;
        }

        public LocacaoDataBuilder ComMarcadorCombustivel(MarcadorCombustivelEnum nivelMarcador)
        {
            locacao.MarcadorCombustivel = nivelMarcador;
            return this;
        }

        public LocacaoDataBuilder ComDataDeDevolucaoRealizada(DateTime data)
        {
            locacao.DataDevolucaoRealizada = data;
            return this;
        }

        public LocacaoDataBuilder DoVeiculo(Veiculo veiculo)
        {
            locacao.RegistrarPara(veiculo);

            return this;
        }

        public LocacaoDataBuilder DoFuncionario(Funcionario funcionario)
        {
            locacao.RegistrarPara(funcionario);
            return this;
        }

        public LocacaoDataBuilder ParaCondutor(Condutor condutor)
        {
            locacao.RegistrarPara(condutor);
            return this;
        }


        public LocacaoDataBuilder ComRelatorioPDF()
        {
            var caminhoPDF = Directory.GetCurrentDirectory() + "\\LocacaoModule\\relatorio.pdf";

            var arquivoCarregado = File.ReadAllBytes(caminhoPDF);

            locacao.AnexarRelatorio(arquivoCarregado);

            return this;
        }
        public Locacao Build()
        {
            return locacao;
        }


    }
}
