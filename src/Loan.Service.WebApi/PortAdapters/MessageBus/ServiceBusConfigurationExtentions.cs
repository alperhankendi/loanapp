using System;
using Loan.Core;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.Service.WebApi
{
    public static class ServiceBusConfigurationExtentions
    {
        /// <summary>
        /// ServiceBus wrapper
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="url">Rabbitmq endpoint</param>
        /// <param name="startImmediately">wanna bus at start time?</param>
        /// <returns></returns>
        public static IServiceCollection UseServiceBus(this IServiceCollection services,string url="rabbitmq://127.0.0.1",bool startImmediately=false)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(url), h => { });
            });
           
            services.AddSingleton<IPublishEndpoint>(bus);
            services.AddSingleton<ISendEndpointProvider>(bus);            
            services.AddSingleton<MassTransit.IBus>(bus);
            
            services.AddSingleton<IEventPublisher, EventPublisher>();
            
            //later phase
            services.AddMassTransit(x =>
            {
                x.AddConsumer<LoanApplicationEventConsumer>();
            });
            
            if (startImmediately)
            {
                bus.StartAsync();
            }
            
            return services;
        }
    }
}