using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: FunctionsStartup(typeof(FunctionApp2.Startup))]
namespace FunctionApp2
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IScoped1>().ImplementedBy<Scoped1>().LifestyleScoped());
               

            // register function classes in container
            var functions = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                t.GetMethods().Any(m => m.GetCustomAttributes(typeof(FunctionNameAttribute), false).Any()));

            foreach (var function in functions)
            {
                container.Register(Component.For(function).LifestyleScoped());
            }

            builder.Services.AddScoped<CastleWindsorJobActivator.ScopeDisposable>()
                .AddSingleton<IJobActivator>(new CastleWindsorJobActivator(container));

            container.AddServices(builder.Services);
        }
    }
}
