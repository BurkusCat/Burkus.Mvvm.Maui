namespace Burkus.Mvvm.Maui;

internal class BurkusMvvmMauiInitializer : IMauiInitializeService
{
    public void Initialize(IServiceProvider serviceProvider)
    {
        var serviceScope = serviceProvider.CreateScope();

        ServiceResolver.RegisterScope(serviceScope);
    }
}
