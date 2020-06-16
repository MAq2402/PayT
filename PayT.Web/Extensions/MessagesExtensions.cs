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
using PayT.Domain.Events;
using PayT.Infrastructure.Events;
using PayT.Web.Dispatchers;

namespace PayT.Web.Extensions
{
    public static class MessagesExtensions
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfigurationSection section)
        {
            var options = new RawRabbitConfiguration();
            section.Bind(options);

            var client = BusClientFactory.CreateDefault(options);
            services.AddSingleton<IBusClient>(_ => client);
        }

        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventDispatcher = app.ApplicationServices.GetAutofacRoot().Resolve<IEventDispatcher>();

            var busClient = app.ApplicationServices.GetService<IBusClient>();

            busClient
                .SubscribeAsync<IEvent>(async (@event, context) =>
                {
                    dynamic handlers = eventDispatcher.DispatchMany(@event);
                    foreach (var handler in handlers)
                    {
                        await handler.HandleAsync((dynamic)@event);
                    }
                });
            return app;
        }
    }
}