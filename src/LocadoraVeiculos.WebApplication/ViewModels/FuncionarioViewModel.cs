using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApplication.ViewModels
{
    public class FuncionarioListViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DateTime DataAdmissao { get; set; }
        public double Salario { get; set; }
    }
    public class FuncionarioIndexViewModel : ITituloViewModel //página de listagem
    {
        public string Titulo => "Funcionario";

        public List<FuncionarioListViewModel> Registros { get; set; }
    }

    public abstract class FuncionarioInputViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DateTime DataAdmissao { get; set; }
        public double Salario { get; set; }
    }
    public class FuncionarioCreateViewModel : FuncionarioInputViewModel, ITituloViewModel
    {
        public string Titulo => "Cadastro de Funcionarios";
    }

    public class FuncionarioEditViewModel : FuncionarioInputViewModel, ITituloViewModel
    {
        public string Titulo => "Edição do Funcionario";

        [Key]
        public int Id { get; set; }
    }

    public abstract class FuncionarioInfoViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DateTime DataAdmissao { get; set; }
        public double Salario { get; set; }
    }

    public class FuncionarioDetailsViewModel : FuncionarioInfoViewModel, ITituloViewModel
    {
        public string Titulo => "Dados do Funcionario";
    }

    public class FuncionarioDeleteViewModel : FuncionarioInfoViewModel, ITituloViewModel
    {
        public string Titulo => "Exclusão do Funcionario";
    }

}
