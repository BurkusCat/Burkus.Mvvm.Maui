namespace Burkus.Mvvm.Maui;

public interface INavigatedEvents
{
    /// <summary>
    /// Triggered when the navigation to a new page is complete.
    /// </summary>
    /// <param name="parameters">Parameters to be passed onto the next page.</param>
    /// <returns>A completed task</returns>
    Task OnNavigatedTo(NavigationParameters parameters);

    /// <summary>
    /// Triggered after you have navigated away from a page. Triggers before
    /// <see cref="OnNavigatedTo(NavigationParameters)"/> triggers for the next page.
    /// </summary>
    /// <param name="parameters">Parameters to be passed onto the next page.</param>
    /// <returns>A completed task</returns>
    Task OnNavigatedFrom(NavigationParameters parameters);
}
