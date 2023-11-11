namespace Burkus.Mvvm.Maui;

public class BurkusNavigationPage : NavigationPage
{
    protected override bool OnBackButtonPressed()
    {
        return BackButtonNavigator.HandleBackButtonPressed();
    }
}
