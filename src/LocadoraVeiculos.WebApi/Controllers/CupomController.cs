using AutoMapper;
using LocadoraVeiculos.Aplicacao.CupomModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.CupomModule;
using LocadoraVeiculos.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
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
    public class CupomController : ControllerBase
    {
        private readonly ICupomRepository cupomRepository;
        private readonly ICupomAppService cupomAppService;
        private readonly IParceiroRepository parceiroRepository;
        private readonly IParceiroAppService parceiroAppService;
        private readonly IMapper mapper;
        private readonly INotificador notificador;

        public CupomController(ICupomAppService cupomAppService, ICupomRepository cupomRepository, IParceiroAppService parceiroAppService,
            IParceiroRepository parceiroRepository, IMapper mapper, INotificador notificador)
        {

            this.cupomAppService = cupomAppService;
            this.cupomRepository = cupomRepository;
            this.parceiroAppService = parceiroAppService;
            this.parceiroRepository = parceiroRepository;
            this.mapper = mapper;
            this.notificador = notificador;
        }


        [HttpGet]
        public List<CupomListViewModel> GetAll()
        {
            var cupons = cupomRepository.SelecionarTodos();

            var viewModel = mapper.Map<List<CupomListViewModel>>(cupons);

            return viewModel;
        }

        [HttpGet("{id}")]
        public ActionResult<CupomDetailsViewModel> Get(int id)
        {
            var cupom = cupomRepository.SelecionarPorId(id);

            if (cupom == null)
                return NotFound(id);

            var viewModel = mapper.Map<CupomDetailsViewModel>(cupom);

            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult<CupomCreateViewModel> Create(CupomCreateViewModel viewModel)
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
            

            Cupom cupom = mapper.Map<Cupom>(viewModel);

            bool registroRealizado = cupomAppService.RegistrarNovoCupom(cupom);

            if (registroRealizado ==false)
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
        public ActionResult<CupomEditViewModel> Edit(int id, CupomEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            Cupom cupom = mapper.Map<Cupom>(viewModel);

            var resultado = cupomAppService.EditarCupom(id, cupom);

            if (resultado == "Cupom editado com sucesso")
            {
                return Ok(viewModel);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CupomCreateViewModel> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id não pode ser menor que 0");

            var resultado = cupomAppService.ExcluirCupom(id);

            if (resultado == "Cupom excluído com sucesso")
            {
                return Ok(id);
            }

            return NoContent();
        }


    }
}
