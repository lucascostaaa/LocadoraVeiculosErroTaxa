using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.ViewModels
{
    public class GrupoVeiculoListViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class GrupoVeiculoDetailsViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public List<CupomListViewModel> Cupons { get; set; }
    }

    public class GrupoVeiculoCreateViewModel
    {
        [Required(ErrorMessage = "O campo Nome é Obrigatório")]
        public string Nome { get; set; }
    }

    public class GrupoVeiculoEditViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
