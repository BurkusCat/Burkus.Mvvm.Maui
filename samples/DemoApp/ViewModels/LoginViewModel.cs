using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    #region Fields

    protected IDialogService dialogService { get; }

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
        INavigationService navigationService)
        : base(navigationService)
    {
        this.dialogService = dialogService;
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

        var navigationParameters = new NavigationParameters
        {
            { "username", Username },
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
        if (string.IsNullOrEmpty(Username))
        {
            dialogService.DisplayAlert(
                "Error",
                "You must enter a username.",
                "OK");

            return false;
        }

        if (string.IsNullOrEmpty(Password))
        {
            dialogService.DisplayAlert(
                "Error",
                "You must enter a password.",
                "OK");

            return false;
        }

        return true;
    }

    #endregion Private methods
}
