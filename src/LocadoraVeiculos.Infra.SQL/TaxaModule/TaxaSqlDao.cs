using LocadoraVeiculos.Dominio.TaxaModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LocadoraVeiculos.Infra.SQL.TaxaModule
{
    public class TaxaSqlDao : RepositorySqlBase<Taxa, int>, ITaxaRepository
    {
        protected override string SqlInserir =>
           @"INSERT INTO TBTAXA
	                (	
		                [NOME]
                       ,[VALOR]
                       ,[TIPOTAXA]
	                )
	                VALUES
	                (
                        @NOME, 
		                @VALOR, 
		                @TIPOTAXA
	                )";

        protected override string SqlSelecionarTodos =>
            @"SELECT 
                   [ID]
                  ,[NOME]
                  ,[VALOR]
                  ,[TIPOTAXA]
              FROM 
                   [TBTAXA]";

        protected override string SqlSelecionarPorId => throw new NotImplementedException();
        protected override string SqlEditar => throw new NotImplementedException();
        protected override string SqlExcluir => throw new NotImplementedException();

        private const string sqlSelecionarNaoAdicionadasFormat =
            @"SELECT 
                   [ID]
                  ,[NOME]
                  ,[VALOR]
                  ,[TIPOTAXA]
              FROM 
                   [TBTAXA] WHERE ID NOT IN ({0}) ";

        public List<Taxa> SelecionarTaxasNaoAdicionadas(List<Taxa> taxasJaAdicionadas)
        {
            List<int> ids = taxasJaAdicionadas.Select(x => x.Id).ToList();

            var parametrosSql = ids.Select((s, i) => "p" + i.ToString()).ToArray();

            string sql = string.Format(sqlSelecionarNaoAdicionadasFormat, ConfigurarFiltroIn(parametrosSql));

            var parametros = new Dictionary<string, object>();

            for (int i = 0; i < parametrosSql.Length; i++)
            {
                parametros.Add(parametrosSql[i], ids[i]);
            }

            return Db.GetAll(sql, Converter, parametros);
        }

        protected override Taxa Converter(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["ID"]);
            var nome = Convert.ToString(reader["NOME"]);
            var valor = Convert.ToDecimal(reader["VALOR"]);

            var tipoTaxa = (TipoTaxaEnum)Enum.Parse(typeof(TipoTaxaEnum), reader["TIPOTAXA"].ToString());

            Taxa taxa = new Taxa(nome, valor, tipoTaxa);

            taxa.Id = id;

            return taxa;
        }

        protected override Dictionary<string, object> ObterParametros(Taxa entity)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", entity.Id);
            parametros.Add("NOME", entity.Nome);
            parametros.Add("VALOR", entity.Valor);
            parametros.Add("TIPOTAXA", (int)entity.TipoTaxa);

            return parametros;
        }

        private static string ConfigurarFiltroIn(string[] parms)
        {
            var inclause = string.Join(",", parms);

            inclause = inclause.Replace(",", ", @");

            if (!inclause.StartsWith("@"))
                inclause = "@" + inclause;

            return inclause;
        }

        public bool VerificarTaxaExistente(string nome)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
