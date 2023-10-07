using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Abstractions;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    #region Fields

    protected IWeatherService weatherService { get; }

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string currentWeatherDescription;

    #endregion Properties

    #region Constructors

    public HomeViewModel(
        INavigationService navigationService,
        IWeatherService weatherService)
        : base(navigationService)
    {
        this.weatherService = weatherService;
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
    /// Logout of the application.
    /// </summary>
    [RelayCommand]
    private async Task Logout()
    {
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
        await navigationService.Navigate($"{nameof(LoginPage)}/{nameof(DemoTabsPage)}/{nameof(RegisterPage)}");
    }

    #endregion Commands
}
