using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using MongoDB.Bson;
using PayT.Domain.Events;
using PayT.Infrastructure.Events;
using PayT.Web.Dispatchers;
using RabbitMQ.Client.Framing.Impl;
using RawRabbit.Context;

namespace PayT.Web.Extensions
{
    public static class MessagesExtensions
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfigurationSection section)
        {
            var options = new RawRabbitConfiguration();
            section.Bind(options);

            var client =
                BusClientFactory.CreateDefault<AdvancedMessageContext>(config => config.AddConfiguration(section));
            services.AddSingleton<IBusClient<AdvancedMessageContext>>(_ => client);
        }

        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventDispatcher = app.ApplicationServices.GetAutofacRoot().Resolve<IEventDispatcher>();

            var busClient = app.ApplicationServices.GetService<IBusClient<AdvancedMessageContext>>();

            busClient
                .SubscribeAsync<IEvent>(async (@event, context) =>
                {
                    if (context.RetryInfo.NumberOfRetries > 3)
                    {

                    }
                    dynamic handlers = eventDispatcher.DispatchMany(@event);
                    foreach (var handler in handlers)
                    {
                        try
                        {
                            await handler.HandleAsync((dynamic) @event);
                        }
                        catch
                        {
                            context.RetryLater(TimeSpan.FromSeconds(5));
                            
                        }
                    }
                });
            return app;
        }
    }
}