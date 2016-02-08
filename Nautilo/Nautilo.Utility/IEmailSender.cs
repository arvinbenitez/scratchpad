namespace Nautilo.Utility
{
    public interface IEmailSender
    {
        void Send(string recipient, string cc, string subject, string message);
    }
}
