using CommunityToolkit.Mvvm.ComponentModel;
using DemoApp.Models;

namespace DemoApp.ViewModels;

[MapNavigationParameter(nameof(ShowLabel), NavigationParameterKeys.ShowLabel)]
[MapNavigationParameter(nameof(LabelText), NavigationParameterKeys.LabelText, required: true)]
[MapNavigationParameter(nameof(FontSize))]
public partial class MapPropertiesViewModel : BaseViewModel
{
    #region Properties

    [ObservableProperty]
    public partial bool ShowLabel { get; set; }

    [ObservableProperty]
    public partial string LabelText { get; set; }

    [ObservableProperty]
    public partial int FontSize { get; set; } = 14;

    #endregion Properties

    #region Constructors

    public MapPropertiesViewModel(
        INavigationService navigationService)
        : base(navigationService)
    {
    }

    #endregion Constructors

    #region Lifecycle events

    // no "OnNavigatedTo" lifecycle code needed to map the properties

    #endregion Lifecycle events
}
