using AutoMapper;
using LocadoraVeiculos.Aplicacao.TaxaModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.TaxaModule;
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
    public class TaxaController : ControllerBase
    {
        private readonly ITaxaRepository taxaRepository;
        private readonly ITaxaAppService taxaAppService;
        private readonly IMapper mapper;
        private readonly INotificador notificador;


        public TaxaController(ITaxaRepository taxaRepository, ITaxaAppService taxaAppService, IMapper mapper, INotificador notificador)
        {
            this.taxaRepository = taxaRepository;
            this.taxaAppService = taxaAppService;
            this.mapper = mapper;
            this.notificador = notificador;
        }

        [HttpGet]
        public List<TaxaListViewModel> GetAll()
        {
            var taxas = taxaRepository.SelecionarTodos();

            var viewModel = mapper.Map<List<TaxaListViewModel>>(taxas);

            return viewModel;
        }

        [HttpGet("{id}")]
        public ActionResult<TaxaDetailsViewModel> Get(int id)
        {
            var taxa = taxaRepository.SelecionarPorId(id);

            if (taxa == null)
                return NotFound(id);

            var viewModel = mapper.Map<TaxaDetailsViewModel>(taxa);

            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult<TaxaCreateViewModel> Create(TaxaCreateViewModel viewModel)
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

            Taxa taxa = mapper.Map<Taxa>(viewModel);

            bool registroRealizado = taxaAppService.RegistrarNovaTaxa(taxa);

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
    }
}
