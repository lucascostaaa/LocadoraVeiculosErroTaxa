using LocadoraVeiculos.Dominio.CupomModule;
using System;
using System.Collections.Generic;
using System.Data;

namespace LocadoraVeiculos.Infra.SQL.CupomModule
{
    public class CupomSqlDao : RepositorySqlBase<Cupom, int>, ICupomRepository
    {
        protected override string SqlInserir =>
           @"INSERT INTO TBCUPOM
	                (	
		                [NOME], 
		                [VALOR], 		                
                        [DATAVALIDADE],
                        [PARCEIRO_ID],
                        [VALORMINIMO],
                        [TIPO]
	                )
	                VALUES
	                (
                        @NOME, 
		                @VALOR, 		                
                        @DATAVALIDADE,
                        @PARCEIRO_ID,
                        @VALORMINIMO,
                        @TIPO
	                )";

        protected override string SqlEditar => throw new NotImplementedException();

        protected override string SqlExcluir => throw new NotImplementedException();

        protected override string SqlSelecionarTodos => throw new NotImplementedException();

        protected override string SqlSelecionarPorId => throw new NotImplementedException();

        private const string sqlSelecionarCuponsAtivos =
            @"SELECT TOP (1000) [ID]
                  ,[NOME]
                  ,[VALOR]
                  ,[DATAVALIDADE]
                  ,[PARCEIRO_ID]
                  ,[VALORMINIMO]
                  ,[TIPO]
              FROM 
                   [TBCUPOM]
              WHERE @DATA < [DATAVALIDADE]";

        protected override Dictionary<string, object> ObterParametros(Cupom cupons)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", cupons.Id);
            parametros.Add("NOME", cupons.Nome);
            parametros.Add("VALOR", cupons.Valor);
            parametros.Add("TIPO", (int)cupons.Tipo);
            parametros.Add("DATAVALIDADE", cupons.DataValidade);
            parametros.Add("VALORMINIMO", cupons.ValorMinimo);
            parametros.Add("PARCEIRO_ID", cupons.Parceiro.Id);
            return parametros;
        }

        public List<Cupom> SelecionarCuponsAtivos(DateTime dataAtual)
        {
            var parametro = new Dictionary<string, object>()
            {
                { "DATA" , dataAtual }
            };

            return Db.GetAll(sqlSelecionarCuponsAtivos, Converter, parametro);
        }

        protected override Cupom Converter(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["ID"]);
            var nome = Convert.ToString(reader["NOME"]);
            var valor = Convert.ToInt32(reader["VALOR"]);

            var parceiroId = Convert.ToInt32(reader["PARCEIRO_ID"]);

            var data = Convert.ToDateTime(reader["DATAVALIDADE"]);
            var valorMinimo = Convert.ToDecimal(reader["VALORMINIMO"]);
            var tipo = (TipoCupomEnum)Enum.Parse(typeof(TipoCupomEnum), reader["TIPO"].ToString());

            Cupom cupom = new Cupom(nome, valor, data, new Parceiro(parceiroId), valorMinimo, tipo);

            cupom.Id = id;

            return cupom;
        }

        public Cupom SelecionarPorId(int id, bool carregarParceiro = false)
        {
            throw new NotImplementedException();
        }

        public bool VerificarCupomExistente(string nome)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
