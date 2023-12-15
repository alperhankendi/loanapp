using System.Text.Json;
using Loan.Core;
using Loan.Domain;
using Loan.Service.Api.PortAdapters.EventConsumers;
using MassTransit;


namespace Loan.Service.Api;
public static class ServiceBusConfigurationExtensions
{
    public static IServiceCollection UseServiceBus(this IServiceCollection services)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };
        
        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<LoanApplicationEventConsumer>();
            cfg.AddBus(BusFactory);
        });
        services.AddSingleton<IEventPublisher, EventPublisher>();

        return services;
    }

    private static IBusControl BusFactory(IBusRegistrationContext arg)
    {
        var busControl= Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.ConfigureJsonSerializerOptions(options => 
            {
                options.PropertyNameCaseInsensitive = true;
                options.IncludeFields = true;
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;        
                return options;
            });
            
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            #region Spesific consumer configuration
            
            cfg.ReceiveEndpoint("Loan-Events", e =>
            {
                e.UseMessageRetry(x=>x.Interval(2,100));
                e.ConfigureConsumer<LoanApplicationEventConsumer>(arg);
                EndpointConvention.Map<Events.V1.LoanApplicationAccepted>(e.InputAddress);
                EndpointConvention.Map<Events.V1.LoanApplicationRejected>(e.InputAddress);
                EndpointConvention.Map<Events.V1.LoanApplicationSubmitted>(e.InputAddress);
            });
            
            #endregion
            cfg.ConfigureEndpoints(arg);
        });

        busControl.StartAsync().ConfigureAwait(false);
        return busControl;
    }
}