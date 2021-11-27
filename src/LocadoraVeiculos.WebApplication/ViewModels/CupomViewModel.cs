using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApplication.ViewModels
{
    public class CupomIndexViewModel : ITituloViewModel
    {
        public string Titulo => "Cupons";

        public List<CupomListViewModel> Registros { get; set; }
    }
    
    public class CupomListViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [DisplayName("Parceiro")]
        public string ParceiroNome { get; set; }

        [DisplayName("Data de Validade")]
        public DateTime DataValidade { get; set; }

        public decimal Valor { get; set; }
    }

    public abstract class CupomInputViewModel
    {        
       
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Parceiro")]
        public int ParceiroId { get; set; }

        public List<SelectListItem> Parceiros { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]        
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Data de Validade")]
        public DateTime DataValidade { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Valor Mínimo")]        
        public decimal ValorMinimo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Tipo { get; set; }
    }

    public class CupomCreateViewModel : CupomInputViewModel, ITituloViewModel
    {
        public string Titulo => "Cadastro de Cupons";

        public CupomCreateViewModel()
        {
            DataValidade = DateTime.Now;
        }
    }

    public class CupomEditViewModel : CupomInputViewModel, ITituloViewModel
    {       
        public string Titulo => "Edição de Cupons";

        [Key]
        public int Id { get; set; }
    }

    public abstract class CupomInfoViewModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Parceiro")]
        public int ParceiroNome { get; set; }

        public string Nome { get; set; }

        public decimal Valor { get; set; }

        [DisplayName("Data de Validade")]
        public DateTime DataValidade { get; set; }

        [DisplayName("Valor Mínimo")]
        public decimal ValorMinimo { get; set; }

        [DisplayName("Tipo")]
        public string Tipo { get; set; }
    }

    public class CupomDetailsViewModel : CupomInfoViewModel, ITituloViewModel
    {
        public string Titulo => "Detalhes do Cupom";        
    }

    public class CupomDeleteViewModel : CupomInfoViewModel, ITituloViewModel
    {
        public string Titulo => "Exclusão do Cupom";
      
    }

}
