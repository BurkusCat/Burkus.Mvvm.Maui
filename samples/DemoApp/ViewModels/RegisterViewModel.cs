using CommunityToolkit.Mvvm.Input;

namespace DemoApp.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    #region Constructors

    public RegisterViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    public override async Task OnNavigatingFrom(NavigationParameters parameters)
    {
        await base.OnNavigatingFrom(parameters);
    }

    #region Commands

    /// <summary>
    /// Go back to the login page.
    /// </summary>
    [RelayCommand]
    private async Task GoBack()
    {
        await navigationService.Pop();
    }

    #endregion Commands
}
