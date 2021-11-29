using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.TaxaModule;
using LocadoraVeiculos.Infra.Logging;
using Serilog;
using System.Collections.Generic;

namespace LocadoraVeiculos.Aplicacao.TaxaModule
{
    public interface ITaxaAppService
    {
        List<Taxa> SelecionarTodos();
        bool RegistrarNovaTaxa(Taxa taxa);
        Taxa SelecionarPorId(int id);
        string ExcluirTaxa(int id);
        string EditarTaxa(int id, Taxa taxaEditada);
        List<Taxa> SelecionarTaxasNaoAdicionadas(List<Taxa> taxasJaAdicionadas);
    }
    public class TaxaAppService : ITaxaAppService
    {

        private const string IdTaxaFormat = "[Id do Taxa: {TaxaId}]";

        private const string TaxaRegistrado_ComSucesso =
            "Taxa registrado com sucesso";

        private const string TaxaNaoRegistrado =
            "Taxa NÃO registrado. Tivemos problemas com a inserção no banco de dados ";

        private const string TaxaNaoEditado =
           "Taxa não editado. Tivemos problemas com a exclusão no banco de dados";

        private const string TaxaEditado_ComSucesso =
            "Taxa editado com sucesso";

        private const string TaxaNaoExcluido =
           "Taxa não excluído. Tivemos problemas com a exclusão no banco de dados";

        private const string TaxaExcluido_ComSucesso =
            "Taxa excluído com sucesso";


        private readonly ITaxaRepository taxaRepository;
        private readonly INotificador notificador;

        public TaxaAppService(ITaxaRepository taxaRepository, INotificador notificador)
        {
            this.taxaRepository = taxaRepository;
            this.notificador = notificador;
        }
        public List<Taxa> SelecionarTaxasNaoAdicionadas(List<Taxa> taxasJaAdicionadas)
        {
            return taxaRepository.SelecionarTaxasNaoAdicionadas(taxasJaAdicionadas);
        }

        public List<Taxa> SelecionarTodos()
        {
            return taxaRepository.SelecionarTodos();
        }

        public bool RegistrarNovaTaxa(Taxa taxa)
        {
            TaxaValidator validator = new TaxaValidator();

            var resultado = validator.Validate(taxa);

            if (resultado.IsValid == false)
            {
                foreach (var erro in resultado.Errors)
                {
                    notificador.RegistrarNotificacao(erro.ErrorMessage);
                }

                return false;
            }
            var nomeTaxaExistente = taxaRepository.VerificarTaxaExistente(taxa.Nome);

            if (nomeTaxaExistente)
            {
                notificador.RegistrarNotificacao($"A Taxa {taxa.Nome} Já está registrado em nossa base");

                return false;
            }

            var taxaInserido = taxaRepository.Inserir(taxa);

            if (taxaInserido == false)
            {
                Log.Logger.Aqui().Warning(TaxaNaoRegistrado + IdTaxaFormat, taxa.Id);

                notificador.RegistrarNotificacao(TaxaNaoRegistrado);

                return false;
            }

            return true;
        }

        public Taxa SelecionarPorId(int id)
        {
            return taxaRepository.SelecionarPorId(id);
        }

        public string ExcluirTaxa(int id)
        {
            var taxaExcluido = taxaRepository.Excluir(id);

            if (taxaExcluido == false)
            {
                Log.Logger.Aqui().Information(TaxaNaoExcluido + IdTaxaFormat, id);

                return TaxaNaoExcluido;
            }

            return TaxaExcluido_ComSucesso;
        }

        public string EditarTaxa(int id, Taxa taxa)
        {
            var taxaEditada = taxaRepository.Editar(id, taxa);

            if (taxaEditada == false)
            {
                Log.Logger.Aqui().Information(TaxaNaoEditado + IdTaxaFormat, id);

                return TaxaNaoEditado;
            }

            return TaxaEditado_ComSucesso;
        }
    }
}
