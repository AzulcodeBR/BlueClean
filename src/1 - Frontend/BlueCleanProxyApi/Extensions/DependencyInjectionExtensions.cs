using System.Reflection;

namespace BlueCleanProxyApi.Extensions;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddAutoDiscoveredServices(this IServiceCollection services)
  {
    var assembly = Assembly.GetExecutingAssembly();

    var types = assembly.GetTypes()
      .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
      .ToList();

    RegistrarServicesPorNamespace(services, types, "BlueCleanProxyApi.Domains.Services", "BlueCleanProxyApi.Domains.Interfaces");
    RegistrarServicesPorNamespace(services, types, "BlueCleanProxyApi.Extensions.Services", "BlueCleanProxyApi.Extensions.Interfaces");

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
