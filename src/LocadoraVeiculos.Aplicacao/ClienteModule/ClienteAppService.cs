using LocadoraVeiculos.Dominio.ClienteModule;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.ClienteModule
{
    public interface IClienteAppService
    {
        List<Cliente> SelecionarTodos(bool carregarCondutores = true);
    }

    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository clienteRepository;

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

    }
}
