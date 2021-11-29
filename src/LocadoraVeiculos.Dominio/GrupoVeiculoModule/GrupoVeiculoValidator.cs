using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Dominio.GrupoVeiculoModule
{
    public class GrupoVeiculoValidator : AbstractValidator<GrupoVeiculo>
    {
        public GrupoVeiculoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().NotNull().WithMessage("O Nome do Grupo Veiculo é obrigatório");
        }

    }
}
