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
using Microsoft.AspNetCore.Builder;
using PayT.Domain.Events;
using PayT.Infrastructure.Events;

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

        public static IApplicationBuilder SubscribeToEvent<T>(this IApplicationBuilder app, IBusClient client,
            ILifetimeScope componentContext)
            where T : IEvent
        {
            client
                .SubscribeAsync<T>(async (msg, context) =>
                {
                    var handlerType = typeof(IEventHandler<>)
                        .MakeGenericType(msg.GetType());

                    var collectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);

                    if (componentContext.IsRegistered(handlerType))
                    {
                        dynamic handlers = componentContext.Resolve(collectionType) as IEnumerable;
                        foreach (var handler in handlers)
                        {
                            await handler.HandleAsync((dynamic) msg);
                        }
                    }
                });
            return app;
        }
    }
}