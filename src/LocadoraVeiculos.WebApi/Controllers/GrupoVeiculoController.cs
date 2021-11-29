using AutoMapper;
using LocadoraVeiculos.Aplicacao.GrupoVeiculoModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
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
    public class GrupoVeiculoController : ControllerBase
    {
        private readonly IGrupoVeiculoAppService grupoVeiculoAppService;
        private readonly IGrupoVeiculoRepository grupoVeiculoRepository;
        private readonly IMapper mapper;
        private readonly INotificador notificador;



        public GrupoVeiculoController(IGrupoVeiculoAppService grupoVeiculoAppService, IGrupoVeiculoRepository grupoVeiculoRepository,
            IMapper mapper, INotificador notificador)
        {
            this.grupoVeiculoAppService = grupoVeiculoAppService;
            this.grupoVeiculoRepository = grupoVeiculoRepository;
            this.mapper = mapper;
            this.notificador = notificador;
        }

        [HttpGet]
        public List<GrupoVeiculoListViewModel> GetAll()
        {
            var grupo = grupoVeiculoRepository.SelecionarTodos();

            var viewModel = mapper.Map<List<GrupoVeiculoListViewModel>>(grupo);

            return viewModel;
        }

        [HttpGet("{id}")]
        public ActionResult<GrupoVeiculoDetailsViewModel> Get(int id)
        {
            var grupo = grupoVeiculoRepository.SelecionarPorId(id);

            if (grupo == null)
                return NotFound(id);

            var viewModel = mapper.Map<GrupoVeiculoDetailsViewModel>(grupo);

            return Ok(viewModel);
        }

        [HttpPost]
        public ActionResult<GrupoVeiculoCreateViewModel> Create(GrupoVeiculoCreateViewModel viewModel)
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

            GrupoVeiculo parceiro = mapper.Map<GrupoVeiculo>(viewModel);

            bool registroRealizado = grupoVeiculoAppService.RegistrarNovoGrupoVeiculos(parceiro);

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
        public ActionResult<GrupoVeiculoEditViewModel> Edit(int id, GrupoVeiculoEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            GrupoVeiculo parceiro = mapper.Map<GrupoVeiculo>(viewModel);

            var resultado = grupoVeiculoAppService.EditarGrupoVeiculo(id, parceiro);

            if (resultado == "Grupo Veiculo editado com sucesso")
            {
                return Ok(viewModel);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<GrupoVeiculoCreateViewModel> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id não pode ser menor que 0");

            var resultado = grupoVeiculoAppService.ExcluirGrupoVeiculo(id);

            if (resultado == "Grupo Veiculo excluído com sucesso")
            {
                return Ok(id);
            }

            return NoContent();
        }

    }


}
