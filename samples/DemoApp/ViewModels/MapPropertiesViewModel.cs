using CommunityToolkit.Mvvm.ComponentModel;
using DemoApp.Models;

namespace DemoApp.ViewModels;

[MapNavigationParameter(nameof(ShowLabel), NavigationParameterKeys.ShowLabel)]
[MapNavigationParameter(nameof(LabelText), NavigationParameterKeys.LabelText, required: true)]
public partial class MapPropertiesViewModel : BaseViewModel
{
    #region Properties

    [ObservableProperty]
    private bool showLabel;

    [ObservableProperty]
    private string labelText;

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
