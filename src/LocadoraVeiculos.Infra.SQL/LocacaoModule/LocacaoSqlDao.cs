using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;

namespace LocadoraVeiculos.Infra.SQL.LocacaoModule
{
    public class LocacaoSqlDao : ILocacaoRepository
    {
        #region Queries
        private const string sqlInserirLocacao =
            @"INSERT INTO [TBLOCACAO]
	                (
                        [FUNCIONARIO_ID]
                       ,[VEICULO_ID]
                       ,[CONDUTOR_ID]
                       ,[PLANOCOBRANCA_ID]
                       ,[MARCADORCOMBUSTIVEL]
                       ,[DATALOCACAO]
                       ,[DATADEVOLUCAOPREVISTA]
                       ,[DATADEVOLUCAOREALIZADA]
                       ,[QUILOMETRAGEMPERCORRIDA]
                       ,[EMABERTO]                       
                       ,[CUPOM_ID]
                       ,[RELATORIO]
	                ) 
	                VALUES
	                (
		                @FUNCIONARIO_ID, 
		                @VEICULO_ID, 
		                @CONDUTOR_ID,
                        @PLANOCOBRANCA_ID, 
                        @MARCADORCOMBUSTIVEL,
                        @DATALOCACAO,
		                @DATADEVOLUCAOPREVISTA,
                        @DATADEVOLUCAOREALIZADA,                        
                        @QUILOMETRAGEMPERCORRIDA,
                        @EMABERTO,                        
                        @CUPOM_ID,                        
                        @RELATORIO
	                )";

        private const string sqlInserirTaxaSelecionada =
            @"INSERT INTO [TBTAXASELECIONADA]
                    (
                       [LOCACAO_ID]
                      ,[TAXA_ID]
                    )
                 VALUES
                    (
                      @LOCACAO_ID,
                      @TAXA_ID
	                )";

        private const string sqlSelecionarTaxasSelecionadas =
            @"SELECT        
	            TX.ID, 
	            TX.NOME, 	
	            TX.VALOR, 
	            TX.TIPOTAXA
            FROM            
	            TBLOCACAO LC
	            INNER JOIN TBTAXASELECIONADA TXS ON LC.ID = TXS.LOCACAO_ID 
	            INNER JOIN TBTAXA AS TX ON TXS.TAXA_ID = TX.ID

            WHERE LC.ID = @ID";

        private const string sqlSelecionarLocacaoCompleta =
            @"SELECT      
  
	            LC.ID AS LOCACAO_ID, 
	            LC.DATALOCACAO AS LOCACAO_DATALOCACAO, 
	            LC.DATADEVOLUCAOPREVISTA AS LOCACAO_DATADEVOLUCAOPREVISTA, 
	            LC.DATADEVOLUCAOREALIZADA AS LOCACAO_DATADEVOLUCAOREALIZADA, 
	            LC.QUILOMETRAGEMPERCORRIDA AS LOCACAO_QUILOMETRAGEMPERCORRIDA, 
	            LC.EMABERTO AS LOCACAO_EMABERTO, 
                LC.MARCADORCOMBUSTIVEL AS LOCACAO_MARCADORCOMBUSTIVEL,  
                LC.RELATORIO AS LOCACAO_RELATORIO, 
	
	            F.ID AS FUNCIONARIO_ID, 
	            F.NOME AS FUNCIONARIO_NOME, 
	            F.USUARIO AS FUNCIONARIO_USUARIO, 
                F.SENHA AS FUNCIONARIO_SENHA, 
	            F.DATAADMISSAO AS FUNCIONARIO_DATAADMISSAO, 
	            F.SALARIO AS FUNCIONARIO_SALARIO, 
	
	            CND.ID AS CONDUTOR_ID, 
	            CND.NOME AS CONDUTOR_NOME, 
	            CND.ENDERECO AS CONDUTOR_ENDERECO, 
                CND.TELEFONE AS CONDUTOR_TELEFONE, 
	            CND.RG AS CONDUTOR_RG, 
	            CND.CPF AS CONDUTOR_CPF, 
	            CND.CNH AS CONDUTOR_CNH, 
	            CND.DATAVALIDADECNH AS CONDUTOR_DATAVALIDADECNH, 
	
	            CLI.ID AS CLIENTE_ID, 
	            CLI.NOME AS CLIENTE_NOME, 
                CLI.ENDERECO AS CLIENTE_ENDERECO, 
	            CLI.TELEFONE AS CLIENTE_TELEFONE, 
	            CLI.RG AS CLIENTE_RG, 
	            CLI.CPF AS CLIENTE_CPF, 
	            CLI.CNPJ AS CLIENTE_CNPJ, 
	            CLI.TIPOPESSOA AS CLIENTE_TIPOPESSOA, 
	            CLI.EMAIL AS CLIENTE_EMAIL,
	
	            VCO.ID AS VEICULO_ID, 
                VCO.PLACA AS VEICULO_PLACA, 
	            VCO.FABRICANTE AS VEICULO_FABRICANTE, 
	            VCO.QTDLITROSTANQUE AS VEICULO_QTDLITROSTANQUE, 
	            VCO.QTDPORTAS AS VEICULO_QTDPORTAS, 
	            VCO.NUMEROCHASSI AS VEICULO_NUMEROCHASSI, 
                VCO.COR AS VEICULO_COR, 
	            VCO.CAPACIDADEOCUPANTES AS VEICULO_CAPACIDADEOCUPANTES, 
	            VCO.ANOFABRICACAO AS VEICULO_ANOFABRICACAO, 
	            VCO.TAMANHOPORTAMALAS AS VEICULO_TAMANHOPORTAMALAS, 
                VCO.TIPOCOMBUSTIVEL AS VEICULO_TIPOCOMBUSTIVEL, 
	            VCO.MODELO AS VEICULO_MODELO, 
	            VCO.QUILOMETRAGEM AS VEICULO_QUILOMETRAGEM, 
                VCO.IMAGEM AS VEICULO_IMAGEM, 
	
	            GRV.ID AS GRUPO_ID, 
	            GRV.NOME AS GRUPO_NOME, 
	
	            PLC.ID AS PLANO_ID, 
                PLC.VALORDIA AS PLANO_VALORDIA, 
	            PLC.KILOMETRAGEMLIVREINCLUSA AS PLANO_KILOMETRAGEMLIVREINCLUSA, 
	            PLC.VALORKMRODADO AS PLANO_VALORKMRODADO, 
	            PLC.TIPOPLANO AS PLANO_TIPOPLANO, 
	
	            CPM.ID AS CUPOM_ID, 
                CPM.NOME AS CUPOM_NOME, 
	            CPM.VALOR AS CUPOM_VALOR, 
	            CPM.DATAVALIDADE AS CUPOM_DATAVALIDADE, 
	            CPM.VALORMINIMO AS CUPOM_VALORMINIMO, 
	            CPM.TIPO AS CUPOM_TIPO, 
	
	            PRC.ID AS PARCEIRO_ID, 
                PRC.NOME AS PARCEIRO_NOME
	
            FROM
	            TBPARCEIRO AS PRC INNER JOIN TBCUPOM AS CPM ON PRC.ID = CPM.PARCEIRO_ID 
	            RIGHT JOIN DBO.TBLOCACAO AS LC 	
	            INNER JOIN DBO.TBFUNCIONARIO AS F ON LC.FUNCIONARIO_ID = F.ID 
	            INNER JOIN DBO.TBCONDUTOR AS CND ON LC.CONDUTOR_ID = CND.ID 
	            INNER JOIN DBO.TBCLIENTE AS CLI ON CND.CLIENTE_ID = CLI.ID 
	            INNER JOIN DBO.TBVEICULO AS VCO ON LC.VEICULO_ID = VCO.ID 	
	            INNER JOIN DBO.TBPLANOCOBRANCA AS PLC ON LC.PLANOCOBRANCA_ID = PLC.ID 
	            INNER JOIN DBO.TBGRUPOVEICULO AS GRV ON PLC.GRUPOVEICULO_ID = GRV.ID 	
	            ON CPM.ID = LC.CUPOM_ID

            WHERE LC.ID = @ID";

        private const string sqlSelecionarTodasLocacoes =
            @"SELECT      
  
	            LC.ID AS LOCACAO_ID, 
	            LC.DATALOCACAO AS LOCACAO_DATALOCACAO, 
	            LC.DATADEVOLUCAOPREVISTA AS LOCACAO_DATADEVOLUCAOPREVISTA, 
	            LC.DATADEVOLUCAOREALIZADA AS LOCACAO_DATADEVOLUCAOREALIZADA, 
	            LC.QUILOMETRAGEMPERCORRIDA AS LOCACAO_QUILOMETRAGEMPERCORRIDA, 
	            LC.EMABERTO AS LOCACAO_EMABERTO, 
                LC.MARCADORCOMBUSTIVEL AS LOCACAO_MARCADORCOMBUSTIVEL, 
                LC.RELATORIO AS LOCACAO_RELATORIO, 
	
	            F.ID AS FUNCIONARIO_ID, 
	            F.NOME AS FUNCIONARIO_NOME, 
	            F.USUARIO AS FUNCIONARIO_USUARIO, 
                F.SENHA AS FUNCIONARIO_SENHA, 
	            F.DATAADMISSAO AS FUNCIONARIO_DATAADMISSAO, 
	            F.SALARIO AS FUNCIONARIO_SALARIO, 
	
	            CND.ID AS CONDUTOR_ID, 
	            CND.NOME AS CONDUTOR_NOME, 
	            CND.ENDERECO AS CONDUTOR_ENDERECO, 
                CND.TELEFONE AS CONDUTOR_TELEFONE, 
	            CND.RG AS CONDUTOR_RG, 
	            CND.CPF AS CONDUTOR_CPF, 
	            CND.CNH AS CONDUTOR_CNH, 
	            CND.DATAVALIDADECNH AS CONDUTOR_DATAVALIDADECNH, 
	
	            CLI.ID AS CLIENTE_ID, 
	            CLI.NOME AS CLIENTE_NOME, 
                CLI.ENDERECO AS CLIENTE_ENDERECO, 
	            CLI.TELEFONE AS CLIENTE_TELEFONE, 
	            CLI.RG AS CLIENTE_RG, 
	            CLI.CPF AS CLIENTE_CPF, 
	            CLI.CNPJ AS CLIENTE_CNPJ, 
	            CLI.TIPOPESSOA AS CLIENTE_TIPOPESSOA, 
	            CLI.EMAIL AS CLIENTE_EMAIL,
	
	            VCO.ID AS VEICULO_ID, 
                VCO.PLACA AS VEICULO_PLACA, 
	            VCO.FABRICANTE AS VEICULO_FABRICANTE, 
	            VCO.QTDLITROSTANQUE AS VEICULO_QTDLITROSTANQUE, 
	            VCO.QTDPORTAS AS VEICULO_QTDPORTAS, 
	            VCO.NUMEROCHASSI AS VEICULO_NUMEROCHASSI, 
                VCO.COR AS VEICULO_COR, 
	            VCO.CAPACIDADEOCUPANTES AS VEICULO_CAPACIDADEOCUPANTES, 
	            VCO.ANOFABRICACAO AS VEICULO_ANOFABRICACAO, 
	            VCO.TAMANHOPORTAMALAS AS VEICULO_TAMANHOPORTAMALAS, 
                VCO.TIPOCOMBUSTIVEL AS VEICULO_TIPOCOMBUSTIVEL, 
	            VCO.MODELO AS VEICULO_MODELO, 
	            VCO.QUILOMETRAGEM AS VEICULO_QUILOMETRAGEM, 
                VCO.IMAGEM AS VEICULO_IMAGEM, 
	
	            GRV.ID AS GRUPO_ID, 
	            GRV.NOME AS GRUPO_NOME, 
	
	            PLC.ID AS PLANO_ID, 
                PLC.VALORDIA AS PLANO_VALORDIA, 
	            PLC.KILOMETRAGEMLIVREINCLUSA AS PLANO_KILOMETRAGEMLIVREINCLUSA, 
	            PLC.VALORKMRODADO AS PLANO_VALORKMRODADO, 
	            PLC.TIPOPLANO AS PLANO_TIPOPLANO, 
	
	            CPM.ID AS CUPOM_ID, 
                CPM.NOME AS CUPOM_NOME, 
	            CPM.VALOR AS CUPOM_VALOR, 
	            CPM.DATAVALIDADE AS CUPOM_DATAVALIDADE, 
	            CPM.VALORMINIMO AS CUPOM_VALORMINIMO, 
	            CPM.TIPO AS CUPOM_TIPO, 
	
	            PRC.ID AS PARCEIRO_ID, 
                PRC.NOME AS PARCEIRO_NOME
	
            FROM
	            TBPARCEIRO AS PRC INNER JOIN TBCUPOM AS CPM ON PRC.ID = CPM.PARCEIRO_ID 
	            RIGHT JOIN DBO.TBLOCACAO AS LC 	
	            INNER JOIN DBO.TBFUNCIONARIO AS F ON LC.FUNCIONARIO_ID = F.ID 
	            INNER JOIN DBO.TBCONDUTOR AS CND ON LC.CONDUTOR_ID = CND.ID 
	            INNER JOIN DBO.TBCLIENTE AS CLI ON CND.CLIENTE_ID = CLI.ID 
	            INNER JOIN DBO.TBVEICULO AS VCO ON LC.VEICULO_ID = VCO.ID 	
	            INNER JOIN DBO.TBPLANOCOBRANCA AS PLC ON LC.PLANOCOBRANCA_ID = PLC.ID 
	            INNER JOIN DBO.TBGRUPOVEICULO AS GRV ON PLC.GRUPOVEICULO_ID = GRV.ID 	
	            ON CPM.ID = LC.CUPOM_ID";

        private const string sqlEditarLocacao =
            @"UPDATE [TBLOCACAO]

               SET [FUNCIONARIO_ID] = @FUNCIONARIO_ID
                  ,[VEICULO_ID] = @VEICULO_ID
                  ,[CONDUTOR_ID] = @CONDUTOR_ID
                  ,[PLANOCOBRANCA_ID] = @PLANOCOBRANCA_ID
                  ,[MARCADORCOMBUSTIVEL] = @MARCADORCOMBUSTIVEL
                  ,[DATALOCACAO] = @DATALOCACAO
                  ,[DATADEVOLUCAOPREVISTA] = @DATADEVOLUCAOPREVISTA
                  ,[DATADEVOLUCAOREALIZADA] = @DATADEVOLUCAOREALIZADA
                  ,[QUILOMETRAGEMPERCORRIDA] = @QUILOMETRAGEMPERCORRIDA
                  ,[EMABERTO] = @EMABERTO
                  ,[CUPOM_ID] = @CUPOM_ID
                  ,[RELATORIO] = @RELATORIO

                 WHERE
                    [ID] = @ID";

        private const string sqlRemoverTaxaSelecionada =
            @"DELETE FROM [TBTAXASELECIONADA]
                WHERE LOCACAO_ID = @LOCACAO_ID AND TAXA_ID = @TAXA_ID";

        private const string sqlExcluirTaxaSelecionada =
            @"DELETE FROM [TBTAXASELECIONADA]
                WHERE LOCACAO_ID = @LOCACAO_ID ";

        private const string sqlExcluirLocacao =
            @"DELETE FROM [TBLOCACAO]
                WHERE ID = @ID";

        #endregion


        public bool Inserir(Locacao locacao)
        {
            try
            {
                Log.Logger.Aqui().Debug("Inserindo Locação... {@Locacao}", locacao);

                locacao.Id = Db.Insert(sqlInserirLocacao, ObtemParametrosLocacao(locacao));

                foreach (Taxa taxa in locacao.TaxasSelecionadas)
                {
                    var parametros = new Dictionary<string, object>()
                    {
                        { "LOCACAO_ID", locacao.Id},
                        { "TAXA_ID", taxa.Id}
                    };

                    if (taxa.EstadoTaxaLocacao == EstadoTaxaLocacaoEnum.Adicionada)
                    {
                        taxa.EstadoTaxaLocacao = EstadoTaxaLocacaoEnum.Gravada;

                        Log.Logger.Aqui().Debug("Inserindo Taxa... {@Taxa}", taxa);

                        Db.Insert(sqlInserirTaxaSelecionada, parametros);

                        Log.Logger.Aqui().Debug("Taxa Inserida com sucesso {@TaxaId}", taxa.Id);
                    }
                }

                Log.Logger.Aqui().Debug("Inserido com sucesso {@LocacaoId}", locacao.Id);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Falha na inserção da locação {@Locacao}", locacao);

                return false;
            }
        }

        public bool Editar(int id, Locacao locacao)
        {
            try
            {
                Log.Logger.Aqui().Debug("Editando Locação... {@Locacao}", locacao);

                locacao.Id = id;

                Db.Update(sqlEditarLocacao, ObtemParametrosLocacao(locacao));

                var taxasAdicionadas = locacao.TaxasAdicionadas();

                foreach (Taxa taxa in taxasAdicionadas)
                {
                    var parametros = new Dictionary<string, object>()
                    {
                        { "LOCACAO_ID", locacao.Id},
                        { "TAXA_ID", taxa.Id}
                    };

                    Log.Logger.Aqui().Debug("Adicionado Taxa... {@Taxa}", taxa);

                    Db.Insert(sqlInserirTaxaSelecionada, parametros);

                    Log.Logger.Aqui().Debug("Taxa Inserida com sucesso {@TaxaId}", taxa.Id);
                }

                var taxasRemovidas = locacao.TaxasRemovidas();

                for (int i = 0; i < taxasRemovidas.Count; i++)
                {
                    Taxa taxa = taxasRemovidas[i];

                    var parametros = new Dictionary<string, object>()
                    {
                        { "LOCACAO_ID", locacao.Id},
                        { "TAXA_ID", taxa.Id}
                    };

                    Log.Logger.Aqui().Debug("Removendo Taxa... {@Taxa}", taxa);

                    Db.Delete(sqlRemoverTaxaSelecionada, parametros);

                    Log.Logger.Aqui().Debug("Taxa Removida com sucesso {@TaxaId}", taxa);

                    locacao.RemoverTaxaLocacao(taxa);
                }

                Log.Logger.Aqui().Debug("Locação editada com sucesso {@LocacaoId}", locacao.Id);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Falha na edição da locação {@Locacao}", locacao);

                return false;
            }
        }

        public bool Excluir(int id)
        {
            try
            {
                Log.Logger.Aqui().Debug("Excluindo Locação {@LocacaoId}", id);

                Db.Delete(sqlExcluirTaxaSelecionada, new Dictionary<string, object>()
                {
                    { "LOCACAO_ID" ,  id }
                });

                Db.Delete(sqlExcluirLocacao, new Dictionary<string, object>()
                {
                    { "ID" ,  id }
                });

                Log.Logger.Aqui().Debug("Locação excluida com sucesso {@LocacaoId}", id);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Falha na exclusão da locação {@LocacaoId}", id);

                return false;
            }
        }

        public bool Excluir(Locacao locacao)
        {
            return true;
        }

        public Locacao SelecionarPorId(int id, bool carregarLocacaoCompleta = true)
        {
            try
            {
                Log.Logger.Aqui().Debug("Selecionando Locação {@LocacaoId}", id);

                var parametro = new Dictionary<string, object>
                    {
                        { "ID", id }
                    };

                var locacao = Db.Get(sqlSelecionarLocacaoCompleta, ConverterEmLocacaoCompleta, parametro);

                Log.Logger.Aqui().Debug("Selecionando Taxas da Locação {@Taxas}", locacao.TaxasSelecionadas?.Select(t => t.Id));

                var taxas = Db.GetAll(sqlSelecionarTaxasSelecionadas, ConverterEmTaxa, parametro);

                foreach (var taxa in taxas)
                {
                    locacao.SelecionarTaxa(taxa);
                }

                Log.Logger.Aqui().Debug("Locação selecionada com sucesso {@LocacaoId}", id);

                return locacao;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Falha na seleção da locação por Id {@LocacaoId}", id);

                return null;
            }
        }

        public List<Locacao> SelecionarTodosPorVeiculo(int idVeiculo, bool carregarTaxas = false)
        {
            try
            {
                Log.Logger.Aqui().Debug("Selecionando Locações por veículo... {@VeiculoId}", idVeiculo);

                var locacoes = SelecionarTodos(carregarTaxas)
                        .Where(x => x.Veiculo.Id == idVeiculo)
                        .ToList();

                Log.Logger.Aqui().Debug("Locações seleciondas por veículo {@VeiculoId}", idVeiculo);

                return locacoes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Falha na seleção das locações por id do veículo {@VeiculoId}", idVeiculo);

                return null;
            }
        }

        public List<Locacao> SelecionarTodos(bool carregarTaxas = false)
        {
            try
            {
                Log.Logger.Aqui().Debug("Selecionando Todas Locações ... ");

                var locacoes = Db.GetAll(sqlSelecionarTodasLocacoes, ConverterEmLocacaoCompleta);

                if (carregarTaxas == false)
                    return locacoes;

                foreach (var locacao in locacoes)
                {
                    var parametro = new Dictionary<string, object>
                    {
                        { "ID", locacao.Id }
                    };

                    var taxas = Db.GetAll(sqlSelecionarTaxasSelecionadas, ConverterEmTaxa, parametro);

                    foreach (var taxa in taxas)
                    {
                        locacao.ConfigurarTaxa(taxa, EstadoTaxaLocacaoEnum.Gravada);
                    }
                }

                Log.Logger.Aqui().Debug("Locações seleciondas com sucesso");

                return locacoes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Falha na seleção das locações");

                return null;
            }
        }

        #region metodos privados

        private Taxa ConverterEmTaxa(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["ID"]);
            var nome = Convert.ToString(reader["NOME"]);
            var valor = Convert.ToDecimal(reader["VALOR"]);

            var tipoTaxa = (TipoTaxaEnum)Enum.Parse(typeof(TipoTaxaEnum), reader["TIPOTAXA"].ToString());

            Taxa taxa = new Taxa(nome, valor, tipoTaxa);
            taxa.Id = id;

            return taxa;
        }

        private Locacao ConverterEmLocacaoCompleta(IDataReader reader)
        {
            Locacao locacao = ConverterEmLocacao(reader);

            locacao.Funcionario = ConverterEmFuncionario(reader);

            locacao.Condutor = ConverterEmCondutor(reader);

            GrupoVeiculo grupoVeiculo = ConverterEmGrupoVeiculo(reader);

            locacao.Veiculo = ConverterEmVeiculo(reader, grupoVeiculo);

            locacao.PlanoCobranca = ConverterEmPlano(reader, grupoVeiculo);

            locacao.Cupom = ConverterEmCupom(reader);

            return locacao;
        }

        private Dictionary<string, object> ObtemParametrosLocacao(Locacao locacao)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", locacao.Id);
            parametros.Add("FUNCIONARIO_ID", locacao.Funcionario.Id);
            parametros.Add("CONDUTOR_ID", locacao.Condutor.Id);
            parametros.Add("VEICULO_ID", locacao.Veiculo.Id);
            parametros.Add("MARCADORCOMBUSTIVEL", locacao.MarcadorCombustivel);
            parametros.Add("DATALOCACAO", locacao.DataLocacao);
            parametros.Add("DATADEVOLUCAOPREVISTA", locacao.DataDevolucaoPrevista);

            if (locacao.DataDevolucaoRealizada == DateTime.MinValue)
                parametros.Add("DATADEVOLUCAOREALIZADA", DBNull.Value);
            else
                parametros.Add("DATADEVOLUCAOREALIZADA", locacao.DataDevolucaoRealizada);

            parametros.Add("QUILOMETRAGEMPERCORRIDA", locacao.QuilometragemPercorrida);
            parametros.Add("EMABERTO", locacao.EmAberto);
            parametros.Add("PLANOCOBRANCA_ID", locacao.PlanoCobranca.Id);

            if (locacao.RelatorioAnexado)
                parametros.Add("RELATORIO", locacao.Relatorio.ToArray());
            else
                parametros.Add("RELATORIO", SqlBinary.Null);

            if (locacao.Cupom != null)
                parametros.Add("CUPOM_ID", locacao.Cupom.Id);
            else
                parametros.Add("CUPOM_ID", DBNull.Value);



            return parametros;
        }

        private Cupom ConverterEmCupom(IDataReader reader)
        {
            if (reader["CUPOM_ID"] == DBNull.Value)
                return null;

            var id = Convert.ToInt32(reader["CUPOM_ID"]);
            var nome = Convert.ToString(reader["CUPOM_NOME"]);
            var valor = Convert.ToInt32(reader["CUPOM_VALOR"]);
            var data = Convert.ToDateTime(reader["CUPOM_DATAVALIDADE"]);
            var valorMinimo = Convert.ToDecimal(reader["CUPOM_VALORMINIMO"]);

            var tipo = (TipoCupomEnum)Enum.Parse(typeof(TipoCupomEnum), reader["CUPOM_TIPO"].ToString());

            Parceiro parceiro = ConverterEmParceiro(reader);

            Cupom cupom = new Cupom(nome, valor, data, parceiro, valorMinimo, tipo);

            cupom.Id = id;

            return cupom;
        }

        private Parceiro ConverterEmParceiro(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["PARCEIRO_ID"]);
            var nome = Convert.ToString(reader["PARCEIRO_NOME"]);

            Parceiro parceiro = new Parceiro(nome);
            parceiro.Id = id;

            return parceiro;
        }

        private PlanoCobranca ConverterEmPlano(IDataReader reader, GrupoVeiculo grupo)
        {
            var tipoPlano = (TipoPlanoCobrancaEnum)Enum.Parse(typeof(TipoPlanoCobrancaEnum), reader["PLANO_TIPOPLANO"].ToString());

            var id = Convert.ToInt32(reader["PLANO_ID"]);
            var valorDia = Convert.ToDecimal(reader["PLANO_VALORDIA"]);
            var valorKmRodado = Convert.ToDecimal(reader["PLANO_VALORKMRODADO"]);
            var kilometragemLivreInclusa = Convert.ToInt32(reader["PLANO_KILOMETRAGEMLIVREINCLUSA"]);

            PlanoCobranca plano;

            switch (tipoPlano)
            {
                case TipoPlanoCobrancaEnum.PlanoDiario:
                    plano = PlanoCobranca.Diario(valorDia, valorKmRodado);
                    break;

                case TipoPlanoCobrancaEnum.PlanoKmControlado:
                    plano = PlanoCobranca.KmControlado(valorDia, kilometragemLivreInclusa, valorKmRodado);
                    break;

                case TipoPlanoCobrancaEnum.PlanoKmLivre:
                    plano = PlanoCobranca.KmLivre(valorDia);
                    break;

                default:
                    plano = null;
                    break;
            }

            plano.Id = id;
            plano.GrupoVeiculo = grupo;
            return plano;
        }

        private Condutor ConverterEmCondutor(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["CONDUTOR_ID"]);
            var nome = Convert.ToString(reader["CONDUTOR_NOME"]);
            var endereco = Convert.ToString(reader["CONDUTOR_ENDERECO"]);
            var telefone = Convert.ToString(reader["CONDUTOR_TELEFONE"]);
            var numeroRg = Convert.ToString(reader["CONDUTOR_RG"]);
            var numeroCpf = Convert.ToString(reader["CONDUTOR_CPF"]);
            var numeroCnh = Convert.ToString(reader["CONDUTOR_CNH"]);
            var dataValidade = Convert.ToDateTime(reader["CONDUTOR_DATAVALIDADECNH"]);

            Cliente cliente = ConverterEmCliente(reader);
            Condutor condutor = new Condutor(nome, endereco, telefone, numeroRg, numeroCpf, numeroCnh, dataValidade, cliente);

            condutor.Id = id;

            return condutor;
        }

        private Cliente ConverterEmCliente(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["CLIENTE_ID"]);
            var nome = Convert.ToString(reader["CLIENTE_NOME"]);
            var endereco = Convert.ToString(reader["CLIENTE_ENDERECO"]);
            var telefone = Convert.ToString(reader["CLIENTE_TELEFONE"]);
            var rg = Convert.ToString(reader["CLIENTE_RG"]);
            var cpf = Convert.ToString(reader["CLIENTE_CPF"]);
            var cnpj = Convert.ToString(reader["CLIENTE_CNPJ"]);
            var tipo = (TipoPessoaEnum)Enum.Parse(typeof(TipoPessoaEnum), reader["CLIENTE_TIPOPESSOA"].ToString());
            var email = Convert.ToString(reader["CLIENTE_EMAIL"]);

            var cliente = new Cliente(nome, endereco, telefone, rg, cpf, cnpj, tipo, email);

            cliente.Id = id;

            return cliente;
        }

        private Funcionario ConverterEmFuncionario(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["FUNCIONARIO_ID"]);
            var nome = Convert.ToString(reader["FUNCIONARIO_NOME"]);
            var usuario = Convert.ToString(reader["FUNCIONARIO_USUARIO"]);
            var senha = Convert.ToString(reader["FUNCIONARIO_SENHA"]);
            var admissao = Convert.ToDateTime(reader["FUNCIONARIO_DATAADMISSAO"]);
            var salario = Convert.ToDouble(reader["FUNCIONARIO_SALARIO"]);

            var funcionario = new Funcionario(nome, usuario, senha, admissao, salario);

            funcionario.Id = id;

            return funcionario;
        }

        private static Locacao ConverterEmLocacao(IDataReader reader)
        {
            Locacao locacao = new Locacao();

            locacao.Id = int.Parse(reader["LOCACAO_ID"].ToString());
            locacao.EmAberto = Convert.ToBoolean(reader["LOCACAO_EMABERTO"]);
            locacao.DataLocacao = Convert.ToDateTime(reader["LOCACAO_DATALOCACAO"]);
            locacao.DataDevolucaoPrevista = Convert.ToDateTime(reader["LOCACAO_DATADEVOLUCAOPREVISTA"]);

            if (reader["LOCACAO_DATADEVOLUCAOREALIZADA"] != DBNull.Value)
                locacao.DataDevolucaoRealizada = Convert.ToDateTime(reader["LOCACAO_DATADEVOLUCAOREALIZADA"]);

            locacao.QuilometragemPercorrida = Convert.ToInt32(reader["LOCACAO_QUILOMETRAGEMPERCORRIDA"]);
            locacao.MarcadorCombustivel = (MarcadorCombustivelEnum)
                Enum.Parse(typeof(MarcadorCombustivelEnum), reader["LOCACAO_MARCADORCOMBUSTIVEL"].ToString());

            if (reader["LOCACAO_RELATORIO"] != DBNull.Value)
                locacao.Relatorio = (byte[])reader["LOCACAO_RELATORIO"];

            return locacao;
        }

        private static Veiculo ConverterEmVeiculo(IDataReader reader, GrupoVeiculo grupoVeiculo)
        {
            var veiculoId = Convert.ToInt32(reader["VEICULO_ID"]);
            var placa = Convert.ToString(reader["VEICULO_PLACA"]);
            var modelo = Convert.ToString(reader["VEICULO_MODELO"]);
            var fabricante = Convert.ToString(reader["VEICULO_FABRICANTE"]);
            var quilometragem = Convert.ToDouble(reader["VEICULO_QUILOMETRAGEM"]);
            var qtdLitrosTanque = Convert.ToInt32(reader["VEICULO_QTDLITROSTANQUE"]);

            var combustivel = (TipoCombustivelEnum)Enum.Parse(typeof(TipoCombustivelEnum), reader["VEICULO_TIPOCOMBUSTIVEL"].ToString());

            Veiculo veiculo = new Veiculo(placa, modelo, fabricante, quilometragem, qtdLitrosTanque, combustivel, grupoVeiculo);

            veiculo.QtdPortas = Convert.ToInt32(reader["VEICULO_QTDPORTAS"]);
            veiculo.NumeroChassi = Convert.ToString(reader["VEICULO_NUMEROCHASSI"]);
            veiculo.Cor = Convert.ToString(reader["VEICULO_COR"]);
            veiculo.CapacidadeOcupantes = Convert.ToInt32(reader["VEICULO_CAPACIDADEOCUPANTES"]);
            veiculo.AnoFabricacao = Convert.ToInt32(reader["VEICULO_ANOFABRICACAO"]);
            veiculo.TamanhoPortaMalas = Convert.ToString(reader["VEICULO_TAMANHOPORTAMALAS"]);
            veiculo.Imagem = (byte[])reader["VEICULO_IMAGEM"];

            veiculo.Id = veiculoId;

            return veiculo;
        }

        private static GrupoVeiculo ConverterEmGrupoVeiculo(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["GRUPO_ID"]);
            var nome = Convert.ToString(reader["GRUPO_NOME"]);

            GrupoVeiculo grupoVeiculo = new GrupoVeiculo(nome);
            grupoVeiculo.Id = id;

            return grupoVeiculo;
        }

        public bool Editar(Locacao entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
