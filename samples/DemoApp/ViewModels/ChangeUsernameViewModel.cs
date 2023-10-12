using Burkus.Mvvm.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Models;
using DemoApp.Properties;

namespace DemoApp.ViewModels;

public partial class ChangeUsernameViewModel : BaseViewModel
{
    #region Fields

    private IPreferences preferences { get; }

    private bool wasAnimatedNavigationUsed;

    #endregion Fields

    #region Properties

    [ObservableProperty]
    private string username;

    #endregion Properties

    #region Constructors

    public ChangeUsernameViewModel(
        INavigationService navigationService,
        IPreferences preferences)
        : base(navigationService)
    {
        this.preferences = preferences;
    }

    #endregion Constructors

    #region Lifecycle events

    public override async Task OnNavigatedTo(NavigationParameters parameters)
    {
        await base.OnNavigatedTo(parameters);

        // store if animated navigation was used to get here
        // so we can do the same style of navigation when leaving the modal
        wasAnimatedNavigationUsed = parameters.UseAnimatedNavigation;
    }

    public override async Task OnNavigatingFrom(NavigationParameters parameters)
    {
        await base.OnNavigatingFrom(parameters);

        if (IsValidUsername())
        {
            // save username as a preference
            preferences.Set(PreferenceKeys.Username, Username);

            // pass 'Username' back regardless if the user presses the button
            // or uses a different method of closing the modal (e.g. Android back button)
            parameters.Add(NavigationParameterKeys.Username, Username);
        }

        // this is a modal, so we need to close it modally
        parameters.UseModalNavigation = true;

        // should we animate closing the modal?
        parameters.UseAnimatedNavigation = wasAnimatedNavigationUsed;
    }

    #endregion Lifecycle events

    #region Commands

    /// <summary>
    /// Pop the modal.
    /// </summary>
    [RelayCommand]
    private async Task Finish()
    {
        // notice how no parameters are being passed, instead OnNavigatedFrom
        // will pass the username back.
        await navigationService.Pop();
    }

    #endregion Commands

    #region Private methods

    private bool IsValidUsername()
    {
        return !string.IsNullOrWhiteSpace(Username);
    }

    #endregion Private methods
}
