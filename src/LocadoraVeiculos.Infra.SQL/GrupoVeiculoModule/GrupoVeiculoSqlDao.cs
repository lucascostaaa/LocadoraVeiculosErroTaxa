using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LocadoraVeiculos.Infra.SQL.GrupoVeiculoModule
{
    public class GrupoVeiculoSqlDao : RepositorySqlBase<GrupoVeiculo, int>, IGrupoVeiculoRepository
    {
        protected override string SqlInserir =>
          @"INSERT INTO [TBGRUPOVEICULO] 
	                (
		                [NOME]
	                ) 
	                VALUES
	                (
                        @NOME
	                )";

        private const string sqlInserirPlanosDoGrupoVeiculo =
           @"INSERT INTO [TBPLANOCOBRANCA]
                (
                   [VALORDIA]
                  ,[KILOMETRAGEMLIVREINCLUSA]
                  ,[VALORKMRODADO]
                  ,[TIPOPLANO]
                  ,[GRUPOVEICULO_ID]
                )
              VALUES
                (
                   @VALORDIA, 
                   @KILOMETRAGEMLIVREINCLUSA,
                   @VALORKMRODADO, 
                   @TIPOPLANO, 
                   @GRUPOVEICULO_ID
                )";

        protected override string SqlSelecionarTodos =>
            @"SELECT 
                     [ID]
                    ,[NOME]
                FROM    
                     [TBGRUPOVEICULO]";

        private const string sqlSelecionarPlanosGrupoVeiculo =
            @"SELECT 
                     [ID]
                    ,[VALORDIA]
                    ,[KILOMETRAGEMLIVREINCLUSA]
                    ,[VALORKMRODADO]
                    ,[TIPOPLANO]
                    ,[GRUPOVEICULO_ID]
                FROM 
                     [TBPLANOCOBRANCA]";

        protected override string SqlEditar => throw new NotImplementedException();

        protected override string SqlExcluir => throw new NotImplementedException();

        protected override string SqlSelecionarPorId => throw new NotImplementedException();

        public override bool Inserir(GrupoVeiculo registro)
        {
            try
            {
                registro.Id = Db.Insert(SqlInserir, ObterParametros(registro));

                foreach (var plano in registro.PlanosCobranca)
                {
                    var parametrosPlanos = new Dictionary<string, object>
                    {
                        { "VALORDIA", plano.ValorDia },
                        { "KILOMETRAGEMLIVREINCLUSA", plano.KilometragemLivreInclusa },
                        { "VALORKMRODADO", plano.ValorKMRodado },
                        { "TIPOPLANO", plano.TipoPlano },
                        { "GRUPOVEICULO_ID", plano.GrupoVeiculo.Id }
                    };

                    plano.Id = Db.Insert(sqlInserirPlanosDoGrupoVeiculo, parametrosPlanos);
                }

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false)
        {
            var grupos = Db.GetAll(SqlSelecionarTodos, Converter);

            if (carregarPlanos == false)
                return grupos;

            var planos = Db.GetAll(sqlSelecionarPlanosGrupoVeiculo, ConverterPlanoCobranca);

            foreach (var item in grupos)
            {
                item.PlanosCobranca = planos.Where(p => p.GrupoVeiculo.Id == item.Id).ToList();
            }

            return grupos;
        }


        protected override GrupoVeiculo Converter(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["ID"]);
            var nome = Convert.ToString(reader["NOME"]);

            GrupoVeiculo grupoVeiculo = new GrupoVeiculo(nome);
            grupoVeiculo.Id = id;

            return grupoVeiculo;
        }

        protected override Dictionary<string, object> ObterParametros(GrupoVeiculo entity)
        {
            return new Dictionary<string, object>
                {
                    { "NOME", entity.Nome }
                };
        }

        private PlanoCobranca ConverterPlanoCobranca(IDataReader reader)
        {
            var tipoPlano = (TipoPlanoCobrancaEnum)Enum.Parse(typeof(TipoPlanoCobrancaEnum), reader["TIPOPLANO"].ToString());

            var id = Convert.ToInt32(reader["ID"]);
            var valorDia = Convert.ToDecimal(reader["VALORDIA"]);
            var valorKmRodado = Convert.ToDecimal(reader["VALORKMRODADO"]);
            var kilometragemLivreInclusa = Convert.ToInt32(reader["KILOMETRAGEMLIVREINCLUSA"]);
            var grupoId = Convert.ToInt32(reader["GRUPOVEICULO_ID"]);

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
            plano.GrupoVeiculo = new GrupoVeiculo { Id = grupoId };

            return plano;
        }

        public bool VerificarNomeExistente(string nome)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
