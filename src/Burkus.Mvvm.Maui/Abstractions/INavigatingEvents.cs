namespace Burkus.Mvvm.Maui;

public interface INavigatingEvents
{
    /// <summary>
    /// Triggered before navigation away from the current page.
    /// </summary>
    /// <param name="parameters">Parameters to be passed onto the next page.</param>
    /// <returns>A completed task</returns>
    Task OnNavigatingFrom(NavigationParameters parameters);
}
