using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.ViewModels
{
    public class ParceiroListViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class ParceiroDetailsViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public List<CupomListViewModel> Cupons { get; set; }
    }

    public class ParceiroCreateViewModel
    {
        [Required(ErrorMessage ="O campo Nome é Obrigatório")]
        public string Nome { get; set; }
    }

    public class ParceiroEditViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
