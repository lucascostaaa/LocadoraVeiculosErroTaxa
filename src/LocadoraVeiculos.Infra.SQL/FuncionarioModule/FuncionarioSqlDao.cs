using LocadoraVeiculos.Dominio.FuncionarioModule;
using System;
using System.Collections.Generic;
using System.Data;

namespace LocadoraVeiculos.Infra.SQL.FuncionarioModule
{
    public class FuncionarioSqlDao : RepositorySqlBase<Funcionario, int>, IFuncionarioRepository
    {
        protected override string SqlInserir =>
         @"INSERT INTO [TBFUNCIONARIO]
	                (
		                [NOME],                        
		                [USUARIO], 
		                [SENHA],
                        [DATAADMISSAO], 
		                [SALARIO]
	                ) 
	                VALUES
	                (
                        @NOME,                         
		                @USUARIO, 
		                @SENHA,
                        @DATAADMISSAO, 
		                @SALARIO
	                )";

        protected override string SqlEditar => throw new NotImplementedException();

        protected override string SqlExcluir => throw new NotImplementedException();

        protected override string SqlSelecionarTodos => throw new NotImplementedException();

        protected override string SqlSelecionarPorId => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Funcionario SelecionarFuncionarioLogado()
        {
            throw new NotImplementedException();
        }

        public bool VerificarUsuarioExistente(string usuario)
        {
            throw new NotImplementedException();
        }

        protected override Funcionario Converter(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, object> ObterParametros(Funcionario registro)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", registro.Id);
            parametros.Add("NOME", registro.Nome);
            parametros.Add("USUARIO", registro.Usuario);
            parametros.Add("SENHA", registro.Senha);
            parametros.Add("DATAADMISSAO", registro.DataAdmissao);
            parametros.Add("SALARIO", registro.Salario);

            return parametros; ;
        }

    }
}
