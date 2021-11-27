using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Dominio.CupomModule
{
    public class CupomValidator : AbstractValidator<Cupom>
    {
        public CupomValidator()
        {
            RuleFor(x => x.Nome)
               .NotEmpty()
                    .WithMessage("O {PropertyName} do cupom é obrigatório e não pode ser vazio")
               .Length(2, 200)
                    .WithMessage("O {PropertyName} do cupom precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(x => x.DataValidade)
                .NotEqual(DateTime.MinValue).NotEqual(DateTime.MaxValue)
                .WithMessage("A Data de Validade do cupom está inválida");

            RuleFor(x => x.ParceiroId)
                .NotEqual(0).WithMessage("O Parceiro do Cupom é obrigatório e não pode ser vazio");

            RuleFor(x => x.ValorMinimo)
                .GreaterThan(0).WithMessage("O Valor Mínimo do Cupom não pode ser menor que {ComparisonValue}");
        }
    }
}

