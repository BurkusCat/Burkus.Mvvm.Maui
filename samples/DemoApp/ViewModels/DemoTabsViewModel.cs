using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class DemoTabsViewModel : BaseViewModel
{
    #region Constructors

    public DemoTabsViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Navigate back to the homepage.
    /// </summary>
    [RelayCommand]
    private async Task GoBack()
    {
        await navigationService.Pop();
    }

    /// <summary>
    /// Navigate to Alpha tab page.
    /// </summary>
    [RelayCommand]
    private async Task SwitchToAlphaTabPage()
    {
        await navigationService.SelectTab<AlphaTabPage>();
    }

    /// <summary>
    /// Navigate to Beta tab page.
    /// </summary>
    [RelayCommand]
    private async Task SwitchToBetaTabPage()
    {
        await navigationService.SelectTab<BetaTabPage>();
    }

    /// <summary>
    /// Navigate to Charlie tab page.
    /// </summary>
    [RelayCommand]
    private async Task SwitchToCharlieTabPage()
    {
        await navigationService.SelectTab<CharlieTabPage>();
    }

    #endregion Commands
}
