namespace Burkus.Mvvm.Maui;

public class BurkusTabbedPage : TabbedPage
{
    protected override bool OnBackButtonPressed()
    {
        return BackButtonNavigator.HandleBackButtonPressed();
    }
}
