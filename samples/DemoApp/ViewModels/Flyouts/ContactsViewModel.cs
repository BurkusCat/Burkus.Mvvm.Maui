using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class ContactsViewModel : BaseViewModel
{
    #region Properties

    [ObservableProperty]
    private List<string> contactNames = new List<string>()
        {
            "Ronan",
            "Claire",
            "Joanne",
            "Dave",
            "Mike",
            "Veronica",
        };

    #endregion Properties

    #region Constructors

    public ContactsViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Commands

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
