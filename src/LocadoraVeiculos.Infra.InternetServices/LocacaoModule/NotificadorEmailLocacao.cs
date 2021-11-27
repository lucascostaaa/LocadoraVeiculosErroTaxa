using LocadoraVeiculos.Dominio.LocacaoModule;
using Serilog;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace LocadoraVeiculos.Infra.InternetServices.LocacaoModule
{
    public class NotificadorEmailLocacao : INotificadorEmailLocacao
    {
        public bool EnviarEmailLocacao(Locacao locacao)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("locadora@gmail.com");

                    mail.Subject = "Relatório de Locação";

                    mail.Attachments.Add(new Attachment(new MemoryStream(locacao.Relatorio), MediaTypeNames.Application.Pdf));

                    //GerarClient(s => s.Send(mail));
                }

                return true;
            }
            catch (System.Exception exc)
            {
                Log.Error(exc, "Falha no envio de Email {Locacao}", locacao);

                return false;
            }
        }
    }
}
