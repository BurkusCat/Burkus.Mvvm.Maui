namespace Burkus.Mvvm.Maui;

public interface IPageVisibilityEvents
{
    /// <summary>
    /// Is triggered by the <see cref="Page.OnAppearing"/> event.
    /// </summary>
    void OnAppearing();

    /// <summary>
    /// Is triggered by the <see cref="Page.OnDisappearing"/> event.
    /// </summary>
    void OnDisappearing();
}
