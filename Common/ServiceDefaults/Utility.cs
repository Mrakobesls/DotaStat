using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceDefaults;

public static class Utility
{
    /// <summary>
    ///     Automatically register services from interfaces on the <see cref="sampleInterface" /> assembly with implementations
    ///     on the <see cref="sampleImplementation" /> assembly.
    /// </summary>
    /// <param name="services">The IServiceCollection for dependency injection.</param>
    /// <param name="sampleInterface">An interface type in the interfaces assembly to register services for.</param>
    /// <param name="sampleImplementation">An implementation type on the implementations assembly to register services for.</param>
    /// <param name="implementationFilter">A filter function for which implementations should be registered automatically.</param>
    /// <example>
    ///     <code>
    ///     Utility.AutomaticallyRegisterServices(
    ///         services: services,
    ///         exampleInterface: typeof(IDataDemoRepository),
    ///         exampleImplementation: typeof(DataDemoRepository),
    ///         implementationFilter: x => x.IsSubclassOf(typeof(Comply365BaseRepository))
    ///     );
    ///     </code>
    /// </example>
    public static void AutomaticallyRegisterServices(
        IServiceCollection services,
        Type sampleInterface,
        Type sampleImplementation,
        Func<Type, bool>? implementationFilter = null
    )
    {
        // get interfaces and implementations assemblies to perform reflection on
        var interfacesAssembly = sampleInterface.Assembly;
        var implementationsAssembly = sampleImplementation.Assembly;

        // get interfaces and implementations
        var interfaceTypes = interfacesAssembly.GetExportedTypes()
            .Where(x => x.IsInterface)
            .ToArray();
        var implementationTypes = implementationsAssembly.GetExportedTypes()
            .Where(x => x.IsClass && x.IsPublic && (implementationFilter == null || implementationFilter(x)))
            .ToArray();

        // for each implementation, AddScoped for all interfaces it is assignable from
        foreach (var implementationType in implementationTypes)
        {
            var implementsInterfaceTypes = interfaceTypes
                .Where(x => x.IsAssignableFrom(implementationType))
                .ToArray();

            foreach (var implementsInterfaceType in implementsInterfaceTypes)
            {
                services.AddScoped(serviceType: implementsInterfaceType, implementationType: implementationType);
            }
        }
    }
}
