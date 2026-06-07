using System.Reflection;

namespace BlueCleanApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Registra automaticamente todas as interfaces e suas implementações dos namespaces especificados
        /// </summary>
        public static IServiceCollection AddAutoDiscoveredServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .ToList();

            RegistrarServicesPorNamespace(services, types, "BlueCleanApi.Domains.Services", "BlueCleanApi.Domains.Interfaces");
            RegistrarServicesPorNamespace(services, types, "BlueCleanApi.Extensions.Services", "BlueCleanApi.Extensions.Interfaces");
            RegistrarServicesPorNamespace(services, types, "BlueCleanApi.Repositories.Services", "BlueCleanApi.Repositories.Interfaces");

            return services;
        }

        private static void RegistrarServicesPorNamespace(
            IServiceCollection services,
            List<Type> types,
            string serviceNamespace,
            string interfaceNamespace)
        {
            var implementations = types
                .Where(t => t.Namespace != null && t.Namespace.StartsWith(serviceNamespace))
                .ToList();

            foreach (var implementation in implementations)
            {
                var interfaces = implementation.GetInterfaces()
                    .Where(i => i.Namespace != null && i.Namespace.StartsWith(interfaceNamespace))
                    .ToList();

                foreach (var @interface in interfaces)
                    services.AddScoped(@interface, implementation);
            }
        }
    }
}
