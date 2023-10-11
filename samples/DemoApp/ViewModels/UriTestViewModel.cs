using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class UriTestViewModel : BaseViewModel
{
    #region Constructors

    public UriTestViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Go back multiple times
    /// </summary>
    [RelayCommand]
    private async Task GoBackMultipleTimes(int backTimes)
    {
        var uriBuilder = new NavigationUriBuilder();

        for (int i = 0; i < backTimes; i++)
        {
            uriBuilder.AddGoBackSegment();
        }

        await navigationService.Navigate(uriBuilder.Build());
    }

    /// <summary>
    /// Switch to the change username page
    /// </summary>
    [RelayCommand]
    private async Task SwitchToChangeUsername()
    {
        var navigationParameters = new NavigationParameters();
        navigationParameters.UseModalNavigation = true;

        var uriBuilder = new NavigationUriBuilder()
            .AddGoBackSegment()
            .AddGoBackSegment()
            .AddGoBackSegment()
            .AddSegment<ChangeUsernamePage>(navigationParameters);

        await navigationService.Navigate(uriBuilder.ToString());
    }

    #endregion Commands
}
