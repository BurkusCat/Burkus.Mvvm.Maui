namespace Burkus.Mvvm.Maui;

public class BurkusMvvmApplication : Application
{
    protected override Window CreateWindow(IActivationState? activationState)
    {
        Current.MainPage = new NavigationPage();

        var burkusMvvmBuilder = ServiceResolver.Resolve<IBurkusMvvmBuilder>();
        var navigationService = ServiceResolver.Resolve<INavigationService>();
        var serviceProvider = ServiceResolver.GetServiceProvider();

        // perform the user's desired initialization logic
        if (burkusMvvmBuilder.onStartFunc != null)
        {
            burkusMvvmBuilder.onStartFunc.Invoke(navigationService, serviceProvider);
        }

        return base.CreateWindow(activationState);
    }
}
