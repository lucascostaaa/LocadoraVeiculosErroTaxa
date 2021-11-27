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
    public class ParceiroController : ControllerBase
    {
        private readonly IParceiroAppService parceiroAppService;
        private readonly IParceiroRepository parceiroRepository;
        private readonly IMapper mapper;
        private readonly INotificador notificador;

        public ParceiroController(IParceiroAppService parceiroAppService,
            IParceiroRepository parceiroRepository, IMapper mapper, INotificador notificador)
        {
            this.parceiroAppService = parceiroAppService;
            this.parceiroRepository = parceiroRepository;
            this.mapper = mapper;
            this.notificador = notificador;
        }

        [HttpGet]
        public List<ParceiroListViewModel> GetAll()
        {
            var parceiros = parceiroRepository.SelecionarTodos();

            var viewModel = mapper.Map<List<ParceiroListViewModel>>(parceiros);

            return viewModel;
        }

        [HttpGet("{id}")]
        public ActionResult<ParceiroDetailsViewModel> Get(int id)
        {
            var parceiro = parceiroRepository.SelecionarPorId(id);

            if (parceiro == null)
                return NotFound(id);

            var viewModel = mapper.Map<ParceiroDetailsViewModel>(parceiro);

            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult<ParceiroCreateViewModel> Create(ParceiroCreateViewModel viewModel)
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

            Parceiro parceiro = mapper.Map<Parceiro>(viewModel);

            bool registroRealizado = parceiroAppService.RegistrarNovoParceiro(parceiro);

            if (registroRealizado == false)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = notificador.ObterNotificacoes()
                });
            }

            return CreatedAtAction(nameof(Create), viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult<ParceiroEditViewModel> Edit(int id, ParceiroEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            Parceiro parceiro = mapper.Map<Parceiro>(viewModel);

            var resultado = parceiroAppService.EditarParceiro(id, parceiro);

            if (resultado == "Parceiro editado com sucesso")
            {
                return Ok(viewModel);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ParceiroCreateViewModel> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id não pode ser menor que 0");

            var resultado = parceiroAppService.ExcluirParceiro(id);

            if (resultado == "Parceiro excluído com sucesso")
            {
                return Ok(id);
            }

            return NoContent();
        }
    }


}
