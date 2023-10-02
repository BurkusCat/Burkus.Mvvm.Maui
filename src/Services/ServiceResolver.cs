namespace Burkus.Mvvm.Maui;

public static class ServiceResolver
{
    private static IServiceScope scope;

    internal static void RegisterScope(
        IServiceScope serviceScope)
    {
        scope = serviceScope;
    }

    internal static IServiceProvider GetServiceProvider()
    {
        return scope.ServiceProvider;
    }

    /// <summary>
    /// Resolves and retrieves an instance of a specified type from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of the instance to retrieve.</typeparam>
    /// <returns>An instance of the specified type.</returns>
    public static T Resolve<T>() where T : class
    {
        return GetServiceProvider().GetRequiredService<T>();
    }

    /// <summary>
    /// Resolves and retrieves an instance of a specified type from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of the instance to retrieve.</typeparam>
    /// <returns>An instance of the specified type.</returns>
    public static object Resolve(Type type)
    {
        return GetServiceProvider().GetRequiredService(type);
    }
}