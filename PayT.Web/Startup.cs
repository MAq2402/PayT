using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PayT.Application.Commands;
using PayT.Application.EventHandlers;
using PayT.Domain.Events;
using PayT.Infrastructure.Events;
using PayT.Infrastructure.EventStore;
using PayT.Infrastructure.Repositories;
using PayT.Web.Extensions;
using RawRabbit;

namespace PayT.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc();

            services.AddMediatR(typeof(CreateSubjectCommand));

            services.AddRabbitMq(Configuration.GetSection("rabbitmq"));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(WriteRepository<>)).As(typeof(IWriteRepository<>));
            builder.RegisterType<ReadRepository>().As<IReadRepository>();
            builder.RegisterType<PayT.Infrastructure.EventStore.EventStore>().As<IEventStore>();

            RegisterEventHandlers(builder);
        }

        public void RegisterEventHandlers(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(InsertSubjectIntoReadModelHandler)))
                .AsClosedTypesOf(typeof(IEventHandler<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var autofacRoot = app.ApplicationServices.GetAutofacRoot();

            if (!(app.ApplicationServices.GetService(typeof(IBusClient)) is IBusClient busClient))
                throw new NullReferenceException();

            app.SubscribeToEvent<IEvent>(busClient, autofacRoot);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
