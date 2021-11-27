using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.FuncionarioModule
{
    public interface IFuncionarioAppService
    {
        List<Funcionario> SelecionarTodos();
        bool RegistrarNovoFuncionario(Funcionario funcionario);
        Funcionario SelecionarPorId(int id);
        string ExcluirFuncionario(int id);
        string EditarFuncionario(int id, Funcionario funcionario);

    }
    public class FuncionarioAppService : IFuncionarioAppService
    {

        private const string IdFuncionarioFormat = "[Id do Funcionario: {FuncionarioId}]";

        private const string FuncionarioRegistrado_ComSucesso =
            "Funcionario registrado com sucesso";

        private const string FuncionarioNaoRegistrado =
            "Funcionario NÃO registrado. Tivemos problemas com a inserção no banco de dados ";


        private const string FuncionarioNaoEditado =
         "Funcionario não editado. Tivemos problemas com a exclusão no banco de dados";

        private const string FuncionarioEditado_ComSucesso =
            "Funcionario editado com sucesso";

        private const string FuncionarioNaoExcluido =
           "Funcionario não excluído. Tivemos problemas com a exclusão no banco de dados";

        private const string FuncionarioExcluido_ComSucesso =
            "Funcionario excluído com sucesso";


        private readonly IFuncionarioRepository funcionarioRepository;
        private readonly INotificador notificador;

        public FuncionarioAppService(IFuncionarioRepository funcionarioRepository, INotificador notificador)
        {
            this.funcionarioRepository = funcionarioRepository;
            this.notificador = notificador;
        }
        public string EditarFuncionario(int id, Funcionario funcionario)
        {
            var funcionarioAlterado = funcionarioRepository.Editar(id, funcionario);

            if(funcionarioAlterado == false)
            {
                Log.Logger.Aqui().Information(FuncionarioNaoEditado + IdFuncionarioFormat, id);

                return FuncionarioNaoEditado;
            }

            return FuncionarioEditado_ComSucesso;
        }

        public string ExcluirFuncionario(int id)
        {
            var funcionarioExcluido = funcionarioRepository.Excluir(id);

            if(funcionarioExcluido == false)
            {
                Log.Logger.Aqui().Information(FuncionarioNaoExcluido + IdFuncionarioFormat, id);

                return FuncionarioNaoExcluido;
            }

            return FuncionarioExcluido_ComSucesso;
        }

        public bool RegistrarNovoFuncionario(Funcionario funcionario)
        {
            FuncionarioValidator funcionarioValidator = new FuncionarioValidator();


            var resultado = funcionarioValidator.Validate(funcionario);

            if (resultado.IsValid == false)
            {
                foreach (var erro in resultado.Errors)
                {
                    notificador.RegistrarNotificacao(erro.ErrorMessage);
                }

                return false;
            }

            var nomeParceiroExistente = funcionarioRepository.VerificarUsuarioExistente(funcionario.Usuario);

            if(nomeParceiroExistente)
            {
                notificador.RegistrarNotificacao($"O {funcionario.Usuario} já está registrado em nossa base");

                return false;
            }

            var funcionarioInserido = funcionarioRepository.Inserir(funcionario);

            if(funcionarioInserido == false)
            {
                Log.Logger.Aqui().Information(FuncionarioNaoRegistrado + IdFuncionarioFormat, funcionario.Id);

                notificador.RegistrarNotificacao(FuncionarioNaoRegistrado);

                return false;
            }

            return true;
        }

        public Funcionario SelecionarPorId(int id)
        {
            return funcionarioRepository.SelecionarPorId(id);
        }

        public List<Funcionario> SelecionarTodos()
        {
            return funcionarioRepository.SelecionarTodos();
        }

    }
}
