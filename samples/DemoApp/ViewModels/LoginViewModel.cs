using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Models;
using DemoApp.Properties;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    #region Fields

    private IDialogService dialogService { get; }

    private IPreferences preferences { get; }

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string password;

    #endregion Properties

    #region Constructors

    public LoginViewModel(
        IDialogService dialogService,
        INavigationService navigationService,
        IPreferences preferences)
        : base(navigationService)
    {
        this.dialogService = dialogService;
        this.preferences = preferences;
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Navigate to the home page of the app.
    /// </summary>
    [RelayCommand]
    private async Task Login()
    {
        if (!IsValidLoginForm())
        {
            // do not navigate if invalid
            return;
        }

        // save username as a preference
        preferences.Set(PreferenceKeys.Username, Username);

        var navigationParameters = new NavigationParameters
        {
            { NavigationParameterKeys.Username, Username },
        };

        // after we login, we replace the stack so the user can't go back to the Login page
        await navigationService.ResetStackAndPush<HomePage>(navigationParameters);
    }

    /// <summary>
    /// Navigate to the register account page.
    /// </summary>
    [RelayCommand]
    private async Task Register()
    {
        await navigationService.Push<RegisterPage>();
    }

    #endregion Commands

    #region Private methods

    private bool IsValidLoginForm()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            dialogService.DisplayAlert(
                Resources.Error,
                Resources.Login_Validation_RequiredUsername,
                Resources.Button_OK);

            return false;
        }

        if (string.IsNullOrEmpty(Password))
        {
            dialogService.DisplayAlert(
                Resources.Error,
                Resources.Login_Validation_RequiredPassword,
                Resources.Button_OK);

            return false;
        }

        return true;
    }

    #endregion Private methods
}
