using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.GrupoVeiculoModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.GrupoVeiculoModule
{
    public interface IGrupoVeiculoAppService
    {
        List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false);

        bool RegistrarNovoGrupoVeiculos(GrupoVeiculo grupoVeiculo);

        GrupoVeiculo SelecionarPorId(int id);
        string ExcluirGrupoVeiculo(int id);

        string EditarGrupoVeiculo(int id, GrupoVeiculo grupoVeiculo);
    }

    public class GrupoVeiculoAppService : IGrupoVeiculoAppService
    {
        private const string IdGrupoVeiculoFormat = "[Id do GrupoVieuclo: {GrupoVeiculoId}]";

        private const string GrupoVeiculoRegistrado_ComSucesso =
            "GrupoVeiculo registrado com sucesso";

        private const string GrupoVeiculooNaoRegistrado =
            "GrupoVeiculo NÃO registrado. Tivemos problemas com a inserção no banco de dados ";


        private const string GrupoVeiculoNaoEditado =
         "GrupoVeiculo não editado. Tivemos problemas com a exclusão no banco de dados";

        private const string GrupoVeiculoEditado_ComSucesso =
            "GrupoVeiculo editado com sucesso";

        private const string GrupoVeiculoNaoExcluido =
           "GrupoVeiculo não excluído. Tivemos problemas com a exclusão no banco de dados";

        private const string GrupoVeiculoExcluido_ComSucesso =
            "GrupoVeiculo excluído com sucesso";




        private readonly IGrupoVeiculoRepository grupoVeiculoRepository;
        private readonly INotificador notificador;

        public GrupoVeiculoAppService(IGrupoVeiculoRepository grupoVeiculoRepository, INotificador notificador)
        {
            this.grupoVeiculoRepository = grupoVeiculoRepository;
            this.notificador = notificador;
        }

        public string EditarGrupoVeiculo(int id, GrupoVeiculo grupoVeiculo)
        {
            var cupomAlterado = grupoVeiculoRepository.Editar(id, grupoVeiculo);

            if (cupomAlterado == false)
            {
                Log.Logger.Aqui().Information(GrupoVeiculoNaoEditado + IdGrupoVeiculoFormat, id);

                return GrupoVeiculoNaoEditado;
            }

            return GrupoVeiculoEditado_ComSucesso;
        }

        public string ExcluirGrupoVeiculo(int id)
        {
            var parceiroExcluido = grupoVeiculoRepository.Excluir(id);

            if (parceiroExcluido == false)
            {
                Log.Logger.Aqui().Information(GrupoVeiculoNaoExcluido + IdGrupoVeiculoFormat, id);

                return GrupoVeiculoNaoExcluido;
            }

            return GrupoVeiculoExcluido_ComSucesso;
        }

        public bool RegistrarNovoGrupoVeiculos(GrupoVeiculo grupoVeiculo)
        {
            GrupoVeiculoValidator validator = new GrupoVeiculoValidator();

            var resultado = validator.Validate(grupoVeiculo);

            if (resultado.IsValid == false)
            {
                foreach (var erro in resultado.Errors)
                {
                    notificador.RegistrarNotificacao(erro.ErrorMessage);
                }

                return false;
            }

            var nomeParceiroExistente = grupoVeiculoRepository.VerificarNomeExistente(grupoVeiculo.Nome);

            if (nomeParceiroExistente)
            {
                notificador.RegistrarNotificacao($"O nome {grupoVeiculo.Nome} já está registrado em nossa base");

                return false;
            }

            var parceiroInserido = grupoVeiculoRepository.Inserir(grupoVeiculo);

            if (parceiroInserido == false)
            {
                Log.Logger.Aqui().Warning(GrupoVeiculooNaoRegistrado + IdGrupoVeiculoFormat, grupoVeiculo.Id);

                notificador.RegistrarNotificacao(GrupoVeiculooNaoRegistrado);

                return false;
            }

            return true;
        }

        public GrupoVeiculo SelecionarPorId(int id)
        {
            return grupoVeiculoRepository.SelecionarPorId(id);
        }

        public List<GrupoVeiculo> SelecionarTodos(bool carregarPlanos = false)
        {
            return grupoVeiculoRepository.SelecionarTodos(carregarPlanos);
        }
    }
}
