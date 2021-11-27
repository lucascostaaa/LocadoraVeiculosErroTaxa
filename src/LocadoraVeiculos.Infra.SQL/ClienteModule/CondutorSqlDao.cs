using LocadoraVeiculos.Dominio.ClienteModule;
using System;
using System.Collections.Generic;
using System.Data;

namespace LocadoraVeiculos.Infra.SQL.ClienteModule
{
    public class CondutorSqlDao : RepositorySqlBase<Condutor, int>, ICondutorRepository
    {
        protected override string SqlInserir =>
            @"INSERT INTO [TBCondutor]
                    (
                         [Nome]
                        ,[Endereco]
                        ,[Telefone]
                        ,[RG]
                        ,[CPF]
                        ,[CNH]
                        ,[DataValidadeCNH]
                        ,[Cliente_Id]
                    )
                    VALUES
                    (
                        @Nome,
                        @Endereco,
                        @Telefone,
                        @RG, 
                        @CPF,
                        @CNH,
                        @DataValidadeCNH,
                        @Cliente_Id
                    )";

        protected override string SqlEditar => throw new NotImplementedException();

        protected override string SqlExcluir => throw new NotImplementedException();

        protected override string SqlSelecionarTodos => throw new NotImplementedException();

        protected override string SqlSelecionarPorId => throw new NotImplementedException();


        private const string sqlSelecionarTodosCondutoresSemCliente =
           @"SELECT 
                CND.ID AS CONDUTOR_ID,
                CND.NOME AS CONDUTOR_NOME,
                CND.ENDERECO AS CONDUTOR_ENDERECO,
                CND.TELEFONE AS CONDUTOR_TELEFONE,
                CND.RG AS CONDUTOR_RG,
                CND.CPF AS CONDUTOR_CPF,
                CND.CNH AS CONDUTOR_CNH,
                CND.DATAVALIDADECNH AS CONDUTOR_DATAVALIDADECNH,
                CND.CLIENTE_ID AS CONDUTOR_CLIENTE_ID
              FROM 
                [DBO].[TBCONDUTOR] CND";

        private const string sqlSelecionarTodosCondutoresComCliente =
           @"SELECT        
                CLI.ID AS CLIENTE_ID,
                CLI.NOME AS CLIENTE_NOME,
                CLI.ENDERECO AS CLIENTE_ENDERECO,
                CLI.TELEFONE AS CLIENTE_TELEFONE,
                CLI.RG AS CLIENTE_RG,
                CLI.CPF AS CLIENTE_CPF,
                CLI.CNPJ AS CLIENTE_CNPJ,
                CLI.TIPOPESSOA AS CLIENTE_TIPOPESSOA,                
                CLI.EMAIL AS CLIENTE_EMAIL,

                CND.ID AS CONDUTOR_ID,
                CND.NOME AS CONDUTOR_NOME,
                CND.ENDERECO AS CONDUTOR_ENDERECO,
                CND.TELEFONE AS CONDUTOR_TELEFONE,
                CND.RG AS CONDUTOR_RG,
                CND.CPF AS CONDUTOR_CPF,
                CND.CNH AS CONDUTOR_CNH,
                CND.DATAVALIDADECNH AS CONDUTOR_DATAVALIDADECNH,
                CND.CLIENTE_ID AS CONDUTOR_CLIENTE_ID
            FROM            
                TBCLIENTE CLI INNER JOIN TBCONDUTOR CND ON CLI.ID = CND.CLIENTE_ID";


        public List<Condutor> SelecionarTodos(bool carregarClientes = false)
        {
            List<Condutor> condutores;

            if (carregarClientes)
                condutores = Db.GetAll(sqlSelecionarTodosCondutoresComCliente, ConverterEmCondutorComCliente);
            else
                condutores = Db.GetAll(sqlSelecionarTodosCondutoresSemCliente, ConverterEmCondutorSemCliente);

            return condutores;
        }

        private Condutor ConverterEmCondutorSemCliente(IDataReader reader)
        {
            return Converter(reader);
        }

        private Condutor ConverterEmCondutorComCliente(IDataReader reader)
        {
            Condutor condutor = Converter(reader);

            int id = Convert.ToInt32(reader["CLIENTE_ID"]);
            string nome = Convert.ToString(reader["CLIENTE_NOME"]);
            string endereco = Convert.ToString(reader["CLIENTE_ENDERECO"]);
            string telefone = Convert.ToString(reader["CLIENTE_TELEFONE"]);
            string rg = Convert.ToString(reader["CLIENTE_RG"]);
            string cpf = Convert.ToString(reader["CLIENTE_CPF"]);
            string cnpj = Convert.ToString(reader["CLIENTE_CNPJ"]);
            string email = Convert.ToString(reader["CLIENTE_EMAIL"]);

            Cliente cliente = new Cliente(nome, endereco, telefone, rg, cpf, cnpj, Dominio.TipoPessoaEnum.Juridica, email);
            cliente.Id = id;

            condutor.Cliente = cliente;

            return condutor;
        }

        protected override Condutor Converter(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["CONDUTOR_ID"]);
            var nome = Convert.ToString(reader["CONDUTOR_NOME"]);
            var endereco = Convert.ToString(reader["CONDUTOR_ENDERECO"]);
            var telefone = Convert.ToString(reader["CONDUTOR_TELEFONE"]);
            var numeroRg = Convert.ToString(reader["CONDUTOR_RG"]);
            var numeroCpf = Convert.ToString(reader["CONDUTOR_CPF"]);
            var numeroCnh = Convert.ToString(reader["CONDUTOR_CNH"]);
            var dataValidade = Convert.ToDateTime(reader["CONDUTOR_DATAVALIDADECNH"]);
            var clienteId = Convert.ToInt32(reader["CONDUTOR_CLIENTE_ID"]);

            Condutor condutor = new Condutor(nome, endereco, telefone, numeroRg, numeroCpf, numeroCnh, dataValidade, new Cliente(clienteId));

            condutor.Id = id;

            return condutor;
        }

        protected override Dictionary<string, object> ObterParametros(Condutor registro)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", registro.Id);
            parametros.Add("NOME", registro.Nome);
            parametros.Add("ENDERECO", registro.Endereco);
            parametros.Add("TELEFONE", registro.Telefone);
            parametros.Add("RG", registro.Rg);
            parametros.Add("CPF", registro.Cpf);
            parametros.Add("CNH", registro.Cnh);
            parametros.Add("DataValidadeCNH", registro.DataValidadeCnh);
            parametros.Add("Cliente_Id", registro.Cliente.Id);

            return parametros;

        }
    }
}
