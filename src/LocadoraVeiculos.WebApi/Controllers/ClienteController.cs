using AutoMapper;
using LocadoraVeiculos.Aplicacao.ClienteModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IClienteAppService clienteAppService;
        private readonly IMapper mapper;
        private readonly INotificador notificador;

        public ClienteController(IClienteRepository clienteRepository, IClienteAppService clienteAppService, IMapper mapper, INotificador notificador)
        {
            this.clienteRepository = clienteRepository;
            this.clienteAppService = clienteAppService;
            this.mapper = mapper;
            this.notificador = notificador;
        }

        [HttpGet]
        public List<ClienteListViewModel> GetAll()
        {
            var taxas = clienteRepository.SelecionarTodos();

            var viewModel = mapper.Map<List<ClienteListViewModel>>(taxas);

            return viewModel;
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDetailsViewModel> Get(int id)
        {
            var taxa = clienteRepository.SelecionarPorId(id);

            if (taxa == null)
                return NotFound(id);

            var viewModel = mapper.Map<ClienteDetailsViewModel>(taxa);

            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult<ClienteCreateViewModel> Create(ClienteCreateViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(x => x.Errors);

                foreach (var erro in erros)
                {
                    var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;

                    notificador.RegistrarNotificacao(erroMsg);
                }

                return BadRequest(new
                {
                    success = false,
                    errors = notificador.ObterNotificacoes()
                });
            }

            Cliente cliente = mapper.Map<Cliente>(viewModel);

            bool registroRealizado = clienteAppService.RegistrarNovoCliente(cliente);

            if (registroRealizado == false)
            {
                return BadRequest(new
                {
                    sucess = false,
                    errors = notificador.ObterNotificacoes()
                }); ;
            }

            return CreatedAtAction(nameof(Create), viewModel);

        }

        [HttpPut("{id}")]
        public ActionResult<ClienteEditViewModel> Edit(int id, ClienteEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            Cliente cliente = mapper.Map<Cliente>(viewModel);

            var resultado = clienteAppService.EditarCliente(id, cliente);

            if (resultado == "Cliente editado com sucesso")
            {
                return Ok(viewModel);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ClienteCreateViewModel> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id não pode ser menor que 0");

            var resultado = clienteAppService.ExcluirCliente(id);

            if (resultado == "Cliente excluído com sucesso")
            {
                return Ok(id);
            }

            return NoContent();
        }

    }

}
