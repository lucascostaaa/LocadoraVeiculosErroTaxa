using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Dominio.TaxaModule
{
    public class TaxaValidator : AbstractValidator<Taxa>
    {
        public TaxaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                 .WithMessage("O {PropertyName} da Taxa é obrigatório e não pode ser vazio")
                .Length(2, 200)
                .WithMessage("O {PropertyName} do cupom precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
