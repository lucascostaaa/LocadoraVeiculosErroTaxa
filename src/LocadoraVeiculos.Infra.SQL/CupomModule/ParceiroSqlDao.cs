using LocadoraVeiculos.Dominio.CupomModule;
using System;
using System.Collections.Generic;
using System.Data;

namespace LocadoraVeiculos.Infra.SQL.CupomModule
{
    public class ParceiroSqlDao : RepositorySqlBase<Parceiro, int>, IParceiroRepository
    {
        protected override string SqlInserir =>
               @"INSERT INTO TBPARCEIRO
	                        (	
		                        [NOME]
	                        )
	                        VALUES
	                        (
                                @NOME
	                        )";

        protected override string SqlEditar => throw new NotImplementedException();

        protected override string SqlExcluir => throw new NotImplementedException();

        protected override string SqlSelecionarTodos => throw new NotImplementedException();

        protected override string SqlSelecionarPorId => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Parceiro SelecionarPorId(int id, bool carregarCupons = true)
        {
            throw new NotImplementedException();
        }

        public bool VerificarNomeExistente(string nome)
        {
            throw new NotImplementedException();
        }

        protected override Parceiro Converter(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, object> ObterParametros(Parceiro parceiro)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", parceiro.Id);
            parametros.Add("NOME", parceiro.Nome);
            ;

            return parametros;
        }
    }
}
