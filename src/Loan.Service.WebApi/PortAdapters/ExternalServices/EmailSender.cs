using Loan.Core;

namespace Loan.Service.WebApi.PortAdapters.ExternalServices
{
    public class EmailSender : IEmailSender
    {
        public void Send(string to, string @from, string message)
        {
            //todo
        }
    }
}