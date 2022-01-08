namespace Loan.Core
{
    public interface IEmailSender
    {
        void Send(string to, string from, string message);
    }
}