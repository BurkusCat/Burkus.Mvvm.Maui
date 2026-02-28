using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoApp.ViewModels;

public partial class PageVisibilityEventViewModel : BaseViewModel, IPageVisibilityEvents
{
    #region Properties

    [ObservableProperty]
    public partial bool ShowLabel { get; set; }

    #endregion Properties

    #region Constructors

    public PageVisibilityEventViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Lifecycle events

    public void OnAppearing()
    {
        ShowLabel = true;
    }

    public void OnDisappearing()
    {
        ShowLabel = false;
    }

    #endregion Lifecycle events
}
