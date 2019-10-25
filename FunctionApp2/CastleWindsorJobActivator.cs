using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp2
{
    public class CastleWindsorJobActivator : IJobActivatorEx
    {
        private readonly WindsorContainer container;

        public CastleWindsorJobActivator(WindsorContainer container) => this.container = container;

        public T CreateInstance<T>(IFunctionInstanceEx functionInstance)
        {
            var disposer = functionInstance.InstanceServices.GetRequiredService<ScopeDisposable>();
            disposer.Scope = container.BeginScope();
            return container.Resolve<T>();
        }

        // Ensures a created Castle.Windsor scope is disposed at the end of the request
        public sealed class ScopeDisposable : IDisposable
        {
            public IDisposable Scope { get; set; }
            public void Dispose() => this.Scope?.Dispose();
        }

        public T CreateInstance<T>()
        {
            var disposer = container.Resolve<ScopeDisposable>();
            disposer.Scope = container.BeginScope();
            return container.Resolve<T>();
        }
    }
}
