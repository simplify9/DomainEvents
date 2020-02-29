using Microsoft.Extensions.DependencyInjection;
using SW.PrimitiveTypes;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SW.DomainEvents
{
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        async public Task Dispatch(IDomainEvent domainEvent)
        {
            var method = typeof(IHandle<>).MakeGenericType(domainEvent.GetType()).GetMethod("Handle");
            var handlers = serviceProvider.GetServices(typeof(IHandle<>).MakeGenericType(domainEvent.GetType()));

            foreach (var handler in handlers)
                await (dynamic)method.Invoke(handler, new object[] { domainEvent });
        }
    }
}
