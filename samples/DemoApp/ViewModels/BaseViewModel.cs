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

    public virtual Task OnNavigatedTo(NavigationParameters parameters)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnNavigatedFrom(NavigationParameters parameters)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnNavigatingFrom(NavigationParameters parameters)
    {
        return Task.CompletedTask;
    }

    #endregion Lifecycle events
}
