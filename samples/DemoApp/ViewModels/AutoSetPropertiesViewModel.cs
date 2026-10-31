using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoApp.ViewModels;

public partial class AutoSetPropertiesViewModel : BaseViewModel
{
    #region Properties

    [ObservableProperty]
    public partial string GreetingText { get; set; }

    [ObservableProperty]
    public partial int RepeatCount { get; set; }

    [ObservableProperty]
    public partial bool IsHighlighted { get; set; }

    #endregion Properties

    #region Constructors

    public AutoSetPropertiesViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Lifecycle events

    // no "OnNavigatedTo" lifecycle code needed, the properties are set automatically from the navigation parameters

    #endregion Lifecycle events
}
