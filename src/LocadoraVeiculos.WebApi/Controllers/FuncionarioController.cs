using AutoMapper;
using LocadoraVeiculos.Aplicacao.FuncionarioModule;
using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.FuncionarioModule;
using LocadoraVeiculos.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        private readonly IFuncionarioAppService funcionarioAppService;
        private readonly IFuncionarioRepository funcionarioRepository;
        private readonly IMapper mapper;
        private readonly INotificador notificador;


        public FuncionarioController(IFuncionarioAppService funcionarioAppService, IFuncionarioRepository funcionarioRepository,
            IMapper mapper, INotificador notificador)
        {

            this.funcionarioAppService = funcionarioAppService;
            this.funcionarioRepository = funcionarioRepository;
            this.mapper = mapper;
            this.notificador = notificador;

        }

        [HttpGet]
        // GET: FuncionarioController
        public List<FuncionarioListViewModel> GetAll()
        {

            var funcionarios = funcionarioRepository.SelecionarTodos();

            var viewModel = mapper.Map<List<FuncionarioListViewModel>>(funcionarios);

            return viewModel;
        }

        [HttpPost]
        public ActionResult<FuncionarioCreateViewModel> Create(FuncionarioCreateViewModel viewModel)
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

            Funcionario funcionario = mapper.Map<Funcionario>(viewModel);

            bool registroRealizado = funcionarioAppService.RegistrarNovoFuncionario(funcionario);

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


        [HttpGet("{id}")]
        public ActionResult<FuncionarioDetailsViewModel> Get(int id)
        {
            var funcionario = funcionarioRepository.SelecionarPorId(id);

            if (funcionario == null)
                return NotFound(id);

            var viewModel = mapper.Map<FuncionarioDetailsViewModel>(funcionario);

            return Ok(viewModel);
        }


        [HttpPut("{id}")]
        public ActionResult<FuncionarioEditViewModel> Edit(int id, FuncionarioEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            Funcionario funcionario = mapper.Map<Funcionario>(viewModel);

            var resultado = funcionarioAppService.EditarFuncionario(id, funcionario);

            if (resultado == "Funcionario editado com sucesso")
            {
                return Ok(viewModel);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<FuncionarioCreateViewModel> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id não pode ser menor que 0");

            var resultado = funcionarioAppService.ExcluirFuncionario(id);

            if (resultado == "Funcionario excluído com sucesso")
            {
                return Ok(id);
            }

            return NoContent();
        }
    }
}
