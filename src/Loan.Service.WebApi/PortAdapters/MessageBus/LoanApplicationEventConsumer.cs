using System;
using System.Threading.Tasks;
using Loan.Domain;
using MassTransit;

namespace Loan.Service.WebApi
{
    public class LoanApplicationEventConsumer :
        IConsumer<Loan.Domain.Events.V1.LoanApplicationAccepted>,
        IConsumer<Loan.Domain.Events.V1.LoanApplicationRejected>
    {
        public Task Consume(ConsumeContext<Events.V1.LoanApplicationAccepted> context)
        {
            Console.WriteLine($"Loan Application is accepted. Application Number is {context.Message.LoanApplicationId}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<Events.V1.LoanApplicationRejected> context)
        {
            Console.WriteLine($"Loan Application is rejected. Application Number is {context.Message.LoanApplicationId}");
            return Task.CompletedTask;
        }
    }
}