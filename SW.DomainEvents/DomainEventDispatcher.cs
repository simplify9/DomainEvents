using Microsoft.Extensions.DependencyInjection;
using SW.PrimitiveTypes;
using System;
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
            var handlers = serviceProvider.GetServices(typeof(IHandle<>).MakeGenericType(domainEvent.GetType()));

            foreach (dynamic handler in handlers)
                await handler.Handle(domainEvent);
        }
    }
}
