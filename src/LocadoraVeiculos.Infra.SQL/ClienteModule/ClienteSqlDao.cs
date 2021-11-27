using LocadoraVeiculos.Dominio.ClienteModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LocadoraVeiculos.Infra.SQL.ClienteModule
{
    public class ClienteSqlDao : RepositorySqlBase<Cliente, int>, IClienteRepository
    {
        private readonly ICondutorRepository condutorRepository;
        public ClienteSqlDao(ICondutorRepository condutorRepository)
        {
            this.condutorRepository = condutorRepository;
        }

        protected override string SqlInserir =>
            @"INSERT INTO [TBCLIENTE]
                (
                    [NOME]
                    ,[ENDERECO]
                    ,[TELEFONE]
                    ,[RG]
                    ,[CPF]
                    ,[CNPJ]
                    ,[TIPOPESSOA]
                    ,[EMAIL]
                )
                VALUES
                (
                    @NOME,
                    @ENDERECO,
                    @TELEFONE,
                    @RG, 
                    @CPF,
                    @CNPJ,
                    @TIPOPESSOA,
                    @EMAIL
	            )";

        protected override string SqlEditar => throw new NotImplementedException();

        protected override string SqlExcluir => throw new NotImplementedException();

        protected override string SqlSelecionarTodos =>
            @"SELECT
                    [ID],
                    [NOME], 
		            [ENDERECO], 
		            [TELEFONE],
                    [RG], 
		            [CPF],
                    [CNPJ],
                    [TIPOPESSOA],
                    [EMAIL]
                FROM 
                    [TBCLIENTE]";

        protected override string SqlSelecionarPorId => throw new NotImplementedException();

        protected override Dictionary<string, object> ObterParametros(Cliente registro)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", registro.Id);
            parametros.Add("NOME", registro.Nome);
            parametros.Add("ENDERECO", registro.Endereco);
            parametros.Add("TELEFONE", registro.Telefone);
            parametros.Add("RG", registro.RG);
            parametros.Add("CPF", registro.CPF);
            parametros.Add("CNPJ", registro.CNPJ);
            parametros.Add("TIPOPESSOA", (int)registro.TipoPessoa);
            parametros.Add("EMAIL", registro.Email);
            return parametros;

        }

        public List<Cliente> SelecionarTodos(bool carregarCondutores = false)
        {
            var clientes = Db.GetAll(SqlSelecionarTodos, Converter);

            if (carregarCondutores == false)
                return clientes;

            var condutores = condutorRepository.SelecionarTodos(carregarClientes: true);

            foreach (var cliente in clientes)
            {
                var condutoresDoCliente = condutores
                    .Where(x => x.Cliente.Equals(cliente))
                    .ToList();

                cliente.AdicionarCondutores(condutores);
            }

            return clientes;
        }

        public bool ExisteClienteComEsteCpf(string cpf)
        {
            throw new System.NotImplementedException();
        }

        protected override Cliente Converter(IDataReader reader)
        {
            int id = Convert.ToInt32(reader["ID"]);
            string nome = Convert.ToString(reader["NOME"]);
            string endereco = Convert.ToString(reader["ENDERECO"]);
            string telefone = Convert.ToString(reader["TELEFONE"]);
            string rg = Convert.ToString(reader["RG"]);
            string cpf = Convert.ToString(reader["CPF"]);
            string cnpj = Convert.ToString(reader["CNPJ"]);
            string email = Convert.ToString(reader["EMAIL"]);

            Cliente cliente = new Cliente(nome, endereco, telefone, rg, cpf, cnpj, Dominio.TipoPessoaEnum.Juridica, email);

            cliente.Id = id;

            return cliente;
        }
    }
}
