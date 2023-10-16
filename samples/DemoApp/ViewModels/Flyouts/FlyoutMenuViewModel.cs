using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoApp.Models;
using DemoApp.Properties;
using DemoApp.Views;

namespace DemoApp.ViewModels;

public partial class FlyoutMenuViewModel : BaseViewModel
{
    #region Properties

    [ObservableProperty]
    private List<FlyoutPageItem> menuPages = new List<FlyoutPageItem>
    {
        new FlyoutPageItem
        {
            Title = Resources.Contacts_Title,
            IconSource = "contacts.png",
            TargetType = typeof(ContactsPage),
        },
        new FlyoutPageItem
        {
            Title = Resources.Todo_Title,
            IconSource = "todo.png",
            TargetType = typeof(TodoPage),
        },
        new FlyoutPageItem
        {
            Title = Resources.Reminders_Title,
            IconSource = "reminders.png",
            TargetType = typeof(RemindersPage),
        },
        new FlyoutPageItem
        {
            Title = Resources.DemoTabs_Title,
            IconSource = "tabs.png",
            TargetType = typeof(DemoTabsPage),
        },
    };

    #endregion Properties

    #region Constructors

    public FlyoutMenuViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Switch the flyout detail to the selected page.
    /// </summary>
    [RelayCommand]
    private void SwitchFlyoutDetailPage(FlyoutPageItem flyoutPageItem)
    {
        if (flyoutPageItem != null)
        {
            navigationService.SwitchFlyoutDetail(flyoutPageItem.TargetType);
        }
    }

    #endregion Commands
}
