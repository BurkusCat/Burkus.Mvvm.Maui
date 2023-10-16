using CommunityToolkit.Mvvm.Input;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class RemindersViewModel : BaseViewModel
{
    #region Constructors

    public RemindersViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Switch the flyout detail to the reminders page.
    /// </summary>
    [RelayCommand]
    private void SwitchFlyoutDetailToContacts()
    {
        navigationService.SwitchFlyoutDetail<ContactsPage>();
    }

    #endregion Commands
}
