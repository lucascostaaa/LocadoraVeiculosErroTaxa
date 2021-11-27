using LocadoraVeiculos.Dominio.LocacaoModule;
using System.Net.NetworkInformation;

namespace LocadoraVeiculos.Infra.InternetServices.LocacaoModule
{
    public class VerificadorConexaoInternet : IVerificadorConexaoInternet
    {
        public bool TemConexaoComInternet()
        {
            string host = "www.google.com.br";

            Ping p = new Ping();

            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


    }
}
