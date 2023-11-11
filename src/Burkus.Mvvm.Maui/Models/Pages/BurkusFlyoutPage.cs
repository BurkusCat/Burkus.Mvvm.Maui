namespace Burkus.Mvvm.Maui;

public class BurkusFlyoutPage : FlyoutPage
{
    protected override bool OnBackButtonPressed()
    {
        return BackButtonNavigator.HandleBackButtonPressed();
    }
}
