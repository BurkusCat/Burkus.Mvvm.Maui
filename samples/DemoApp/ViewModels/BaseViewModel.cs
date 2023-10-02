using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoApp.ViewModels;

public abstract class BaseViewModel : ObservableObject, INavigatedEvents, INavigatingEvents
{
    #region Fields

    protected INavigationService navigationService { get; }

    #endregion Fields

    #region Constructors

    public BaseViewModel(
        INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    #endregion Constructors

    #region Lifecycle events

    public virtual async Task OnNavigatedTo(NavigationParameters parameters)
    {
    }

    public virtual async Task OnNavigatedFrom(NavigationParameters parameters)
    {
    }

    public virtual async Task OnNavigatingFrom(NavigationParameters parameters)
    {
    }

    #endregion Lifecycle events
}
