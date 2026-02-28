using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Abstractions;
using DemoApp.Models;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    #region Fields

    private IPreferences preferences { get; }

    private IWeatherService weatherService { get; }

    #endregion Fields

    #region Properties

    [ObservableProperty]
    public partial string Username { get; set; }

    [ObservableProperty]
    public partial string CurrentWeatherDescription { get; set; }

    #endregion Properties

    #region Constructors

    public HomeViewModel(
        INavigationService navigationService,
        IPreferences preferences,
        IWeatherService weatherService)
        : base(navigationService)
    {
        this.preferences = preferences;
        this.weatherService = weatherService;
    }

    #endregion Constructors

    #region Lifecycle events

    public override async Task OnNavigatedTo(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        if (parameters.ContainsKey(NavigationParameterKeys.Username))
        {
            Username = parameters.GetValue<string>(NavigationParameterKeys.Username);
        }
        else
        {
            // load the username from preferences
            Username = preferences.Get<string>(PreferenceKeys.Username, default);
        }

        CurrentWeatherDescription = weatherService.GetWeatherDescription();
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

    /// <summary>
    /// Navigate to the example tabbed page and select Beta tab.
    /// </summary>
    [RelayCommand]
    private async Task GoToTabbedPageDemoBetaTab()
    {
        var navigationParameters = new NavigationParameters();
        navigationParameters.SelectTab = nameof(BetaTabPage);

        await navigationService.Push<DemoTabsPage>(navigationParameters);
    }

    /// <summary>
    /// Logout of the application.
    /// </summary>
    [RelayCommand]
    private async Task Logout()
    {
        // remove the username preference
        preferences.Remove(PreferenceKeys.Username);

        // use the navigate URI syntax to logout with an absolute URI
        await navigationService.Navigate("/LoginPage");
    }

    /// <summary>
    /// Add multiple pages onto the stack.
    /// </summary>
    [RelayCommand]
    private async Task AddMultiplePages()
    {
        // use the navigate URI syntax to add multiple pages
        await navigationService.Navigate($"{nameof(DemoTabsPage)}/{nameof(RegisterPage)}/{nameof(UriTestPage)}");
    }

    /// <summary>
    /// Navigate to the example flyout page.
    /// </summary>
    [RelayCommand]
    private async Task GoToFlyoutPageDemo()
    {
        await navigationService.Navigate($"{nameof(DemoFlyoutPage)}");
    }

    /// <summary>
    /// Navigate to the demo page visibility events page.
    /// </summary>
    [RelayCommand]
    private async Task GoToPageVisibilityEventsDemo()
    {
        await navigationService.Push<PageVisibilityEventPage>();
    }

    /// <summary>
    /// Navigate to the map properties demo and pass three parameters.
    /// </summary>
    [RelayCommand]
    private async Task GoToMapPropertiesDemoWithRequiredParameter()
    {
        var navigationParameters = new NavigationParameters
        {
            { NavigationParameterKeys.ShowLabel, true },
            { NavigationParameterKeys.LabelText, "This text has been mapped for you" },
            { nameof(MapPropertiesViewModel.FontSize), 48 },
        };

        await navigationService.Push<MapPropertiesPage>(navigationParameters);
    }

    /// <summary>
    /// Navigate to the map properties demo without all required parameters.
    /// </summary>
    [RelayCommand]
    private async Task GoToMapPropertiesDemoWithoutRequiredParameter()
    {
        var navigationParameters = new NavigationParameters
        {
            { NavigationParameterKeys.ShowLabel, true },
            { nameof(MapPropertiesViewModel.FontSize), 48 },
        };

        await navigationService.Push<MapPropertiesPage>(navigationParameters);
    }

    /// <summary>
    /// Exit the application.
    /// </summary>
    [RelayCommand]
    private async Task Exit()
    {
        // Pop the homepage off the stack, closing the app.
        await navigationService.GoBack();
    }

    #endregion Commands
}
