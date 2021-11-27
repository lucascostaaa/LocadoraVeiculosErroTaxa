namespace LocadoraVeiculos.Dominio.LocacaoModule
{
    public interface IGeradorRelatorioLocacao
    {
        /// <summary>
        /// Método responsável pela geração de relatórios no formato PDF na locação de veículos 
        /// </summary>
        /// <param name="locacao">Locação de Veículo</param>
        /// <returns>Relatório PDF em serializado</returns>
        byte[] GerarRelatorioPdf(Locacao locacao);

    }


}
