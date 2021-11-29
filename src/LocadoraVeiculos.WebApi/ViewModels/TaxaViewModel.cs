using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.ViewModels
{
    public class TaxaListViewModel
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
        public int TipoTaxa { get; set; }
        public int EstadoTaxaLocacao { get; set; }
    }
    public class TaxaDetailsViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public int TipoTaxa { get; set; }
        public int EstadoTaxaLocacao { get; set; }
    }

    public class TaxaCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        public string Nome { get; set; }
        public double Valor { get; set; }
        public int TipoTaxa { get; set; }
        public int EstadoTaxaLocacao { get; set; }
    }

    public class TaxaEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo valor é obrigatório.")]
        public double Valor { get; set; }

        public int TipoTaxa { get; set; }

        public int EstadoTaxaLocacao { get; set; }
    }
}
