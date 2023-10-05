using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    #region Properties

    [ObservableProperty]
    private string username;

    #endregion Properties

    #region Constructors

    public HomeViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Lifecycle events

    public override async Task OnNavigatedTo(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        var usernameValue = parameters.GetValue<string>("username");

        if (!string.IsNullOrWhiteSpace(usernameValue))
        {
            Username = usernameValue;
        }
    }

    #endregion Lifecycle events

    #region Commands

    /// <summary>
    /// Navigate to change username modally without animation.
    /// </summary>
    [RelayCommand]
    private async Task ChangeUsernameWithoutAnimation()
    {
        var navigationParameters = new NavigationParameters();
        navigationParameters.UseModalNavigation = true;
        navigationParameters.UseAnimatedNavigation = false;

        await navigationService.Push<ChangeUsernamePage>(navigationParameters);
    }

    /// <summary>
    /// Navigate to change username modally with animation.
    /// </summary>
    [RelayCommand]
    private async Task ChangeUsernameWithAnimation()
    {
        var navigationParameters = new NavigationParameters();
        navigationParameters.UseModalNavigation = true;

        await navigationService.Push<ChangeUsernamePage>(navigationParameters);
    }

    /// <summary>
    /// Navigate to the example tabbed page.
    /// </summary>
    [RelayCommand]
    private async Task GoToTabbedPageDemo()
    {
        await navigationService.Push<DemoTabsPage>();
    }

    #endregion Commands
}
