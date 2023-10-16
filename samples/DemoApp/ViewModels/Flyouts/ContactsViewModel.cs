using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class ContactsViewModel : BaseViewModel
{
    #region Properties

    [ObservableProperty]
    private ObservableCollection<string> contactNames;

    #endregion Properties

    #region Constructors

    public ContactsViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
        var mockContactNames = new List<string>()
        {
            "Ronan",
            "Claire",
            "Joanne",
            "Dave",
            "Mike",
            "Veronica",
        };

        contactNames = new ObservableCollection<string>(mockContactNames);
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
