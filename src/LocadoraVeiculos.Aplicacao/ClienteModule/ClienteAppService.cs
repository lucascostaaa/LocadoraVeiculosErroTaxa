using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.ClienteModule
{
    public interface IClienteAppService
    {
        List<Cliente> SelecionarTodos(bool carregarCondutores = true);

        bool RegistrarNovoCliente(Cliente cliente);
        Cliente SelecionarPorId(int id);
        string ExcluirCliente(int id);
        string EditarCliente(int id, Cliente clienteEditada);

    }

    public class ClienteAppService : IClienteAppService
    {

        private const string IdClienteFormat = "[Id do Taxa: {ClienteId}]";

        private const string ClienteRegistrado_ComSucesso =
            "Cliente registrado com sucesso";

        private const string ClienteNaoRegistrado =
            "Cliente NÃO registrado. Tivemos problemas com a inserção no banco de dados ";

        private const string ClienteNaoEditado =
           "Cliente não editado. Tivemos problemas com a exclusão no banco de dados";

        private const string ClienteEditado_ComSucesso =
            "Cliente editado com sucesso";

        private const string ClienteeNaoExcluido =
           "Cliente não excluído. Tivemos problemas com a exclusão no banco de dados";

        private const string ClienteExcluido_ComSucesso =
            "Cliente excluído com sucesso";


        private readonly IClienteRepository clienteRepository;
        private readonly INotificador notificador;

        public ClienteAppService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public List<Cliente> SelecionarTodos(bool carregarCondutores = true)
        {
            return clienteRepository.SelecionarTodos(carregarCondutores);
        }

        public string InserirNovoCliente(Cliente cliente)
        {


            if (clienteRepository.ExisteClienteComEsteCpf(cliente.CPF))
            {
                return "Este CPF já está cadastrado";
            }

            string resultadoValidacao = cliente.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
                clienteRepository.Inserir(cliente);

            return resultadoValidacao;
        }

        public bool RegistrarNovoCliente(Cliente cliente)
        {
            ClienteValidator validator = new ClienteValidator();

            var resultado = validator.Validate(cliente);

            if (resultado.IsValid == false)
            {
                foreach (var erro in resultado.Errors)
                {
                    notificador.RegistrarNotificacao(erro.ErrorMessage);
                }

                return false;
            }
            var nomeTaxaExistente = clienteRepository.ExisteClienteComEsteCpf(cliente.CPF);

            if (nomeTaxaExistente)
            {
                notificador.RegistrarNotificacao($"Este CPF {cliente.CPF} Já está registrado em nossa base");

                return false;
            }

            var taxaInserido = clienteRepository.Inserir(cliente);

            if (taxaInserido == false)
            {
                Log.Logger.Aqui().Warning(ClienteNaoRegistrado + IdClienteFormat, cliente.Id);

                notificador.RegistrarNotificacao(ClienteNaoRegistrado);

                return false;
            }

            return true;
        }

        public Cliente SelecionarPorId(int id)
        {
            return clienteRepository.SelecionarPorId(id);
        }

        public string ExcluirCliente(int id)
        {
            var clienteExcluido = clienteRepository.Excluir(id);

            if (clienteExcluido == false)
            {
                Log.Logger.Aqui().Information(ClienteeNaoExcluido + IdClienteFormat, id);

                return ClienteeNaoExcluido;
            }

            return ClienteExcluido_ComSucesso;
        }

        public string EditarCliente(int id, Cliente clienteEditada)
        {
            var cliente = clienteRepository.Editar(id, clienteEditada);

            if (cliente == false)
            {
                Log.Logger.Aqui().Information(ClienteNaoEditado + IdClienteFormat, id);

                return ClienteNaoEditado;
            }

            return ClienteEditado_ComSucesso;
        }
    }
}
