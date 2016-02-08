using System.Net.Mail;
using System.Net;

namespace Nautilo.Utility.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;
        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Send(string recipient, string cc, string subject, string message)
        {
            var client = new SmtpClient();
            client.Host = configuration.GetConfig<string>(ConfigurationCode.SmtpServer);
            client.Port = configuration.GetConfig<int>(ConfigurationCode.SmtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(configuration.GetConfig<string>(ConfigurationCode.SmtpEmail), configuration.GetConfig<string>(ConfigurationCode.SmtpPassword));
            client.EnableSsl = true;
            client.Send(configuration.GetConfig<string>(ConfigurationCode.SmtpEmail), recipient, subject, message);
        }
    }
}
