

using Microsoft.Extensions.DependencyInjection;
using SW.PrimitiveTypes;
using System;
using System.Reflection;

namespace SW.DomainEvents
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0) assemblies = new Assembly[] { Assembly.GetCallingAssembly() };

            serviceCollection.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IHandle<>)))
                .AsImplementedInterfaces().WithScopedLifetime());

            serviceCollection.AddScoped<DomainEventDispatcher>();

            return serviceCollection;
        }

    }
}
