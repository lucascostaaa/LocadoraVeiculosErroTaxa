using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.CupomModule
{
    public interface IParceiroAppService
    {
        List<Parceiro> SelecionarTodos();
        bool RegistrarNovoParceiro(Parceiro parceiro);
        Parceiro SelecionarPorId(int id);
        string ExcluirParceiro(int id);

        string EditarParceiro(int id, Parceiro parceiro);
    }

    public class ParceiroAppService : IParceiroAppService
    {
        private const string IdParceiroFormat = "[Id do Parceiro: {ParceiroId}]";

        private const string ParceiroRegistrado_ComSucesso =
            "Parceiro registrado com sucesso";

        private const string ParceiroNaoRegistrado =
            "Parceiro NÃO registrado. Tivemos problemas com a inserção no banco de dados ";


        private const string ParceiroNaoEditado =
         "Parceiro não editado. Tivemos problemas com a exclusão no banco de dados";

        private const string ParceiroEditado_ComSucesso =
            "Parceiro editado com sucesso";

        private const string ParceiroNaoExcluido =
           "Parceiro não excluído. Tivemos problemas com a exclusão no banco de dados";

        private const string ParceiroExcluido_ComSucesso =
            "Parceiro excluído com sucesso";


        private readonly IParceiroRepository parceiroRepository;
        private readonly INotificador notificador;

        public ParceiroAppService(IParceiroRepository parceiroRepository, INotificador notificador)
        {
            this.parceiroRepository = parceiroRepository;
            this.notificador = notificador;
        }

        public bool RegistrarNovoParceiro(Parceiro parceiro)
        {
            ParceiroValidator validator = new ParceiroValidator();

            var resultado = validator.Validate(parceiro);

            if (resultado.IsValid == false)
            {
                foreach (var erro in resultado.Errors)
                {
                    notificador.RegistrarNotificacao(erro.ErrorMessage);
                }

                return false;
            }

            var nomeParceiroExistente = parceiroRepository.VerificarNomeExistente(parceiro.Nome);

            if (nomeParceiroExistente)
            {
                notificador.RegistrarNotificacao($"O nome {parceiro.Nome} já está registrado em nossa base");

                return false;
            }

            var parceiroInserido = parceiroRepository.Inserir(parceiro);

            if (parceiroInserido == false)
            {
                Log.Logger.Aqui().Warning(ParceiroNaoRegistrado + IdParceiroFormat, parceiro.Id);

                notificador.RegistrarNotificacao(ParceiroNaoRegistrado);

                return false;
            }

            return true;
        }

        public string EditarParceiro(int id, Parceiro parceiro)
        {
            var cupomAlterado = parceiroRepository.Editar(id, parceiro);

            if (cupomAlterado == false)
            {
                Log.Logger.Aqui().Information(ParceiroNaoEditado + IdParceiroFormat, id);

                return ParceiroNaoEditado;
            }

            return ParceiroEditado_ComSucesso;
        }
        public string ExcluirParceiro(int id)
        {
            var parceiroExcluido = parceiroRepository.Excluir(id);

            if (parceiroExcluido == false)
            {
                Log.Logger.Aqui().Information(ParceiroNaoExcluido + IdParceiroFormat, id);

                return ParceiroNaoExcluido;
            }

            return ParceiroExcluido_ComSucesso;
        }



        public Parceiro SelecionarPorId(int id)
        {
            return parceiroRepository.SelecionarPorId(id);
        }

        public List<Parceiro> SelecionarTodos()
        {
            return parceiroRepository.SelecionarTodos();
        }
    }
}
