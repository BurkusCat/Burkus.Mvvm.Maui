﻿using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

/// <summary>
/// All three tabs (Alpha/Beta/Charlie), share this viewmodel.
/// </summary>
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
        // navigate back using the URI syntax
        await navigationService.Navigate("..");
    }

    /// <summary>
    /// Navigate to Alpha tab page.
    /// </summary>
    [RelayCommand]
    private void SwitchToAlphaTabPage()
    {
        navigationService.SelectTab<AlphaTabPage>();
    }

    /// <summary>
    /// Navigate to Beta tab page.
    /// </summary>
    [RelayCommand]
    private void SwitchToBetaTabPage()
    {
        navigationService.SelectTab<BetaTabPage>();
    }

    /// <summary>
    /// Navigate to Charlie tab page.
    /// </summary>
    [RelayCommand]
    private void SwitchToCharlieTabPage()
    {
        navigationService.SelectTab<CharlieTabPage>();
    }

    #endregion Commands
}
