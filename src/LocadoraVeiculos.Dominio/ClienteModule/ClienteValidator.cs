using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Dominio.ClienteModule
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                     .WithMessage("O {PropertyName} do cupom é obrigatório e não pode ser vazio")
                .Length(2, 200)
                      .WithMessage("O {PropertyName} do cupom precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(x => x.CPF)
                 .NotEmpty()
                 .WithMessage("O {PropertyName} do cliente é obrigatório e não pode ser vazio")
                 .Length(2, 200);

            RuleFor(x => x.RG)
                .NotEmpty()
                .WithMessage("O {PropertyName} do cliente é obrigatório e não pode ser vazio")
                .Length(2, 200);

            RuleFor(x => x.Telefone)
                 .NotEmpty()
                 .WithMessage("O {PropertyName} do cliente é obrigatório e não pode ser vazio")
                  .Length(2, 200);


        }
    }
}
