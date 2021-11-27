using LocadoraVeiculos.Dominio.LocacaoModule;
using Serilog;
using System;

namespace LocadoraVeiculos.Infra.PDF.LocacaoModule
{
    public class GeradorRelatorioLocacao : IGeradorRelatorioLocacao
    {

        public byte[] GerarRelatorioPdf(Locacao locacao)
        {
            try
            {
                return new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Falha na geração do relatório PDF da locação {@locacao}", locacao);

                return null;
            }
        }


    }
}
