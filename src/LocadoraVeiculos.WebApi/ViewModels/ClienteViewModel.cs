using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraVeiculos.WebApi.ViewModels
{
    public class ClienteListViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public int TipoPessoa { get; set; }

    }

    public class ClienteDetailsViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public int TipoPessoa { get; set; }
    }

    public class ClienteCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo endereco é obrigatório.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo telefone é obrigatório.")]
        public string Telefone { get; set; }
        public string RG { get; set; }

        [Required(ErrorMessage = "Campo cpf é obrigatório.")]
        public string CPF { get; set; }
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório.")]
        public string Email { get; set; }
        public int TipoPessoa { get; set; }
    }

    public class ClienteEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo endereco é obrigatório.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo telefone é obrigatório.")]
        public string Telefone { get; set; }
        public string RG { get; set; }

        [Required(ErrorMessage = "Campo cpf é obrigatório.")]
        public string CPF { get; set; }
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório.")]
        public string Email { get; set; }
        public int TipoPessoa { get; set; }
    }
}
