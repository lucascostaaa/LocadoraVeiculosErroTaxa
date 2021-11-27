using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio.LocacaoModule;
using LocadoraVeiculos.Dominio.VeiculoModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LocadoraVeiculos.Infra.SQL.VeiculoModule
{
    public class VeiculoSqlDao : RepositorySqlBase<Veiculo, int>, IVeiculoRepository
    {
        private readonly ILocacaoRepository locacaoRepository;

        public VeiculoSqlDao(ILocacaoRepository locacaoRepository)
        {
            this.locacaoRepository = locacaoRepository;
        }

        protected override string SqlInserir =>
           @"INSERT INTO [TBVEICULO]
	                (
                        [PLACA]
                       ,[FABRICANTE]
                       ,[QTDLITROSTANQUE]
                       ,[QTDPORTAS]
                       ,[NUMEROCHASSI]
                       ,[COR]
                       ,[CAPACIDADEOCUPANTES]
                       ,[ANOFABRICACAO]
                       ,[TAMANHOPORTAMALAS]
                       ,[TIPOCOMBUSTIVEL]
                       ,[GRUPOVEICULO_ID]
                       ,[IMAGEM]
                       ,[MODELO]
                       ,[QUILOMETRAGEM]
	                ) 
	                VALUES
	                (
                        @PLACA,                        
		                @FABRICANTE,
                        @QTDLITROSTANQUE,
		                @QTDPORTAS,                        
                        @NUMEROCHASSI,
                        @COR, 
		                @CAPACIDADEOCUPANTES,
                        @ANOFABRICACAO,
                        @TAMANHOPORTAMALAS,
                        @TIPOCOMBUSTIVEL,
                        @GRUPOVEICULO_ID,
                        @IMAGEM,
                        @MODELO,
                        @QUILOMETRAGEM
	                )";
        protected override string SqlEditar =>
            @"UPDATE [TBVEICULO]
               SET
                   [PLACA] = @PLACA
                  ,[FABRICANTE] = @FABRICANTE
                  ,[QTDLITROSTANQUE] = @QTDLITROSTANQUE
                  ,[QTDPORTAS] = @QTDPORTAS
                  ,[NUMEROCHASSI] = @NUMEROCHASSI
                  ,[COR] = @COR
                  ,[CAPACIDADEOCUPANTES] = @CAPACIDADEOCUPANTES
                  ,[ANOFABRICACAO] = @ANOFABRICACAO
                  ,[TAMANHOPORTAMALAS] = @TAMANHOPORTAMALAS
                  ,[TIPOCOMBUSTIVEL] = @TIPOCOMBUSTIVEL
                  ,[GRUPOVEICULO_ID] = @GRUPOVEICULO_ID
                  ,[IMAGEM] = @IMAGEM
                  ,[MODELO] = @MODELO
                  ,[QUILOMETRAGEM] = @QUILOMETRAGEM
             WHERE
                   [ID] = @ID";
        protected override string SqlSelecionarTodos =>
                @"SELECT 	
	               V.[ID]
                  ,V.[PLACA]
                  ,V.[FABRICANTE]
                  ,V.[QTDLITROSTANQUE]
                  ,V.[QTDPORTAS]
                  ,V.[NUMEROCHASSI]
                  ,V.[COR]
                  ,V.[CAPACIDADEOCUPANTES]
                  ,V.[ANOFABRICACAO]
                  ,V.[TAMANHOPORTAMALAS]
                  ,V.[TIPOCOMBUSTIVEL]                  
                  ,V.[IMAGEM]
                  ,V.[MODELO]
                  ,V.[QUILOMETRAGEM]
                  ,G.[ID] AS GRUPOVEICULO_ID 
	              ,G.[NOME] 
              FROM 	
	            [TBVEICULO] V 
              INNER JOIN TBGRUPOVEICULO G ON G.ID = V.GRUPOVEICULO_ID";
        protected override string SqlSelecionarPorId =>
            @"SELECT 	
	               V.[ID]
                  ,V.[PLACA]
                  ,V.[FABRICANTE]
                  ,V.[QTDLITROSTANQUE]
                  ,V.[QTDPORTAS]
                  ,V.[NUMEROCHASSI]
                  ,V.[COR]
                  ,V.[CAPACIDADEOCUPANTES]
                  ,V.[ANOFABRICACAO]
                  ,V.[TAMANHOPORTAMALAS]
                  ,V.[TIPOCOMBUSTIVEL]                  
                  ,V.[IMAGEM]
                  ,V.[MODELO]
                  ,V.[QUILOMETRAGEM]
                  ,G.[ID] AS GRUPOVEICULO_ID 
	              ,G.[NOME] 
              FROM 	
	              [TBVEICULO] V 
                    INNER JOIN TBGRUPOVEICULO G ON G.ID = V.GRUPOVEICULO_ID
                  WHERE  V.[ID]=@ID";
        protected override string SqlExcluir =>
            @"DELETE FROM [TBVEICULO]               
                WHERE
                   [ID] = @ID";

        public List<Veiculo> SelecionarTodos(bool carregarLocacoes = false)
        {
            var veiculos = Db.GetAll(SqlSelecionarTodos, Converter);

            if (carregarLocacoes == false)
                return veiculos;

            var locacoes = locacaoRepository.SelecionarTodos();

            foreach (var veiculo in veiculos)
            {
                var locacoesEncontradas = locacoes
                    .Where(x => x.Veiculo.Equals(veiculo))
                    .ToList();

                veiculo.RegistrarLocacoes(locacoesEncontradas);
            }

            return veiculos;
        }

        protected override Veiculo Converter(IDataReader reader)
        {
            var veiculoId = Convert.ToInt32(reader["ID"]);
            var placa = Convert.ToString(reader["PLACA"]);
            var modelo = Convert.ToString(reader["MODELO"]);
            var fabricante = Convert.ToString(reader["FABRICANTE"]);
            var quilometragem = Convert.ToDouble(reader["QUILOMETRAGEM"]);
            var qtdLitrosTanque = Convert.ToInt32(reader["QTDLITROSTANQUE"]);

            var combustivel = (TipoCombustivelEnum)Enum.Parse(typeof(TipoCombustivelEnum), reader["TIPOCOMBUSTIVEL"].ToString());

            var grupoId = Convert.ToInt32(reader["GRUPOVEICULO_ID"]);
            var nome = Convert.ToString(reader["NOME"]);

            var grupoVeiculo = new GrupoVeiculo
            {
                Id = grupoId,
                Nome = nome
            };

            Veiculo veiculo = new Veiculo(placa, modelo, fabricante, quilometragem, qtdLitrosTanque, combustivel, grupoVeiculo);

            veiculo.QtdPortas = Convert.ToInt32(reader["QTDPORTAS"]);
            veiculo.NumeroChassi = Convert.ToString(reader["NUMEROCHASSI"]);
            veiculo.Cor = Convert.ToString(reader["COR"]);
            veiculo.CapacidadeOcupantes = Convert.ToInt32(reader["CAPACIDADEOCUPANTES"]);
            veiculo.AnoFabricacao = Convert.ToInt32(reader["ANOFABRICACAO"]);
            veiculo.TamanhoPortaMalas = Convert.ToString(reader["TAMANHOPORTAMALAS"]);
            veiculo.Imagem = (byte[])reader["IMAGEM"];

            veiculo.Id = veiculoId;

            return veiculo;
        }

        protected override Dictionary<string, object> ObterParametros(Veiculo veiculo)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", veiculo.Id);
            parametros.Add("PLACA", veiculo.Placa);
            parametros.Add("MODELO", veiculo.Modelo);
            parametros.Add("FABRICANTE", veiculo.Fabricante);
            parametros.Add("QUILOMETRAGEM", veiculo.Quilometragem);
            parametros.Add("QTDLITROSTANQUE", veiculo.QtdLitrosTanque);
            parametros.Add("QTDPORTAS", veiculo.QtdPortas);
            parametros.Add("NUMEROCHASSI", veiculo.NumeroChassi);
            parametros.Add("COR", veiculo.Cor);
            parametros.Add("CAPACIDADEOCUPANTES", veiculo.CapacidadeOcupantes);
            parametros.Add("ANOFABRICACAO", veiculo.AnoFabricacao);
            parametros.Add("TAMANHOPORTAMALAS", veiculo.TamanhoPortaMalas);
            parametros.Add("TIPOCOMBUSTIVEL", (int)veiculo.TipoCombustivel);
            parametros.Add("GRUPOVEICULO_ID", veiculo.GrupoVeiculo.Id);
            parametros.Add("IMAGEM", veiculo.Imagem);


            return parametros;
        }
    }
}
