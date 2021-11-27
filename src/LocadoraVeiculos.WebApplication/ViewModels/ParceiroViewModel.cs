using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.WebApplication.ViewModels
{
    #region list
    public class ParceiroListViewModel //colunas tabela
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class ParceiroIndexViewModel : ITituloViewModel //página de listagem
    {
        public string Titulo => "Parceiros";

        public List<ParceiroListViewModel> Registros { get; set; }
    }

    #endregion

    #region input
    public abstract class ParceiroInputViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
    }

    public class ParceiroCreateViewModel : ParceiroInputViewModel, ITituloViewModel
    {
        public string Titulo => "Cadastro de Parceiros";
    }

    public class ParceiroEditViewModel : ParceiroInputViewModel, ITituloViewModel
    {
        public string Titulo => "Edição do Parceiro";

        [Key]
        public int Id { get; set; }
    }

    #endregion

    #region info
    public abstract class ParceiroInfoViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class ParceiroDetailsViewModel : ParceiroInfoViewModel, ITituloViewModel
    {
        public string Titulo => "Dados do Parceiro";
    }

    public class ParceiroDeleteViewModel : ParceiroInfoViewModel, ITituloViewModel
    {
        public string Titulo => "Exclusão do Parceiro";
    }

    #endregion
}