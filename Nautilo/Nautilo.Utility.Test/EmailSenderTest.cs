using NUnit.Framework;
using Nautilo.Utility.Implementation;
using Moq;

namespace Nautilo.Utility.Test
{

    [TestFixture]
    public class EmailSenderTest
    {
        private EmailSender emailSender;
        private Mock<IConfiguration> configuration;

        [SetUp]
        public void Init()
        {
            configuration = new Mock<IConfiguration>();
            emailSender = new EmailSender(configuration.Object);
        }

        [Test, Ignore("Do not use up any more email from SMTP2GO")]
        public void Send_When_Valid_Should_Send_Email()
        {
            configuration.Setup(x => x.GetConfig<string>(ConfigurationCode.SmtpServer)).Returns("mail.smtp2go.com");
            configuration.Setup(x => x.GetConfig<string>(ConfigurationCode.SmtpEmail)).Returns("arvin.benitez@gmail.com");
            configuration.Setup(x => x.GetConfig<string>(ConfigurationCode.SmtpPassword)).Returns("@quak3sh");
            configuration.Setup(x => x.GetConfig<int>(ConfigurationCode.SmtpPort)).Returns(2525);

            emailSender.Send("arvin.benitez@gmail.com", "", "Hello World", "Hello From Aquakesh.com");
        }

    }
}
