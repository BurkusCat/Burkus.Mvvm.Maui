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
}
