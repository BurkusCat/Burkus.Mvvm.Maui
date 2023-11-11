namespace Burkus.Mvvm.Maui;

/// <summary>
/// Internal Burkus.Mvvm.Maui use only. Not intended for consuming project usage.
/// </summary>
public interface IBurkusMvvmBuilder
{
    /// <summary>
    /// Internal Burkus.Mvvm.Maui use only. Not intended for consuming project usage.
    /// </summary>
    Func<INavigationService, IServiceProvider, Task> onStartFunc { get; set; }
}
