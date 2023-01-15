using Loan.Core;
using Loan.Domain;
using MassTransit;

namespace Loan.Service.Api.PortAdapters.EventConsumers;

public class LoanApplicationEventConsumer :
    IConsumer<Loan.Domain.Events.V1.LoanApplicationAccepted>,
    IConsumer<Loan.Domain.Events.V1.LoanApplicationRejected>,
    IConsumer<Loan.Domain.Events.V1.LoanApplicationSubmitted>
{
    private readonly IEmailSender emailSender;
    private readonly ILogger<LoanApplicationEventConsumer> logger;

    public LoanApplicationEventConsumer(IEmailSender emailSender,ILogger<LoanApplicationEventConsumer> logger)
    {
        this.emailSender = emailSender;
        this.logger = logger;
    }
    public Task Consume(ConsumeContext<Events.V1.LoanApplicationAccepted> context)
    {
        emailSender.Send("","","");
        logger.LogInformation($"Loan Application is accepted. Application Number is {context.Message.LoanApplicationId}");
        return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<Events.V1.LoanApplicationRejected> context)
    {
        emailSender.Send("","","");
        logger.LogInformation($"Loan Application is rejected. Application Number is {context.Message.LoanApplicationId}");
        return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<Events.V1.LoanApplicationSubmitted> context)
    {
        emailSender.Send(context.Message.Email,"","We got your application, we will get back to you.");
        logger.LogInformation($"Load Application is submitted. email: {context.Message.Email}");
        return Task.CompletedTask;
    }
}