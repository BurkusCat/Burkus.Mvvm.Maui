using CommunityToolkit.Mvvm.Input;

namespace DemoApp.ViewModels;

public partial class TodoViewModel : BaseViewModel
{
    #region Constructors

    public TodoViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Push a new page to this detail on top of this one.
    /// </summary>
    [RelayCommand]
    private void PushNewPageToFlyoutDetail()
    {
        // TODO: implement this command
    }

    #endregion Commands
}
