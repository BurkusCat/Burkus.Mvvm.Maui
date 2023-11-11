namespace Burkus.Mvvm.Maui;

internal static class BackButtonNavigator
{
    internal static bool HandleBackButtonPressed()
    {
        var navigationService = ServiceResolver.Resolve<INavigationService>();

        _ = navigationService.GoBack();

        // On Android and Windows, prevent the default back button behaviour
        return true;
    }
}
