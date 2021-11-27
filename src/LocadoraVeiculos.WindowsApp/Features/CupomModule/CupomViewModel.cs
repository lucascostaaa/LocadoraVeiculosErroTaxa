using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.WindowsApp.Features.CupomModule
{
    public class ParceiroListViewModel
    {
        public int Id { get; set; }

        public int Nome { get; set; }
    }
    public class CupomCreateViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public decimal ValorMinimo { get; set; }

        public DateTime DataValidade { get; set; }

        public int ParceiroSelecionadoId { get; set; }

        public List<ParceiroListViewModel> Parceiros { get; set; }

        public int Tipo { get; set; }
    }
}
