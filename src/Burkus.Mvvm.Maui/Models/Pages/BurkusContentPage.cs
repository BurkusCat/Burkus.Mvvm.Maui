namespace Burkus.Mvvm.Maui;

public class BurkusContentPage : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        return BackButtonNavigator.HandleBackButtonPressed();
    }
}
