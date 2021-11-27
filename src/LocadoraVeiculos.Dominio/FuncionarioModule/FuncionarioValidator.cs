using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Dominio.FuncionarioModule
{
    public class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        public FuncionarioValidator()
        {
            RuleFor(x => x.Nome)
                 .NotEmpty().NotNull().WithMessage("O Nome do Funcionario é obrigatório");

            RuleFor(x => x.Usuario)
                 .NotEmpty().NotNull().WithMessage("O Usuario do Funcionario é obrigatório");

            RuleFor(x => x.Senha)
                 .NotEmpty().NotNull().WithMessage("A senha do Funcionario é obrigatório");

            RuleFor(x => x.Salario)
                .NotEmpty().NotNull().WithMessage("O Salario do Funcionario é obrigatório");
        }
    }
}
