namespace Burkus.Mvvm.Maui;

/// <summary>
/// Internal builder based on <see cref="BurkusMvvmBuilder"/>.
/// </summary>
internal class InternalBurkusMvvmBuilder : BurkusMvvmBuilder, IBurkusMvvmBuilder
{
    public Func<INavigationService, IServiceProvider, Task> onStartFunc { get; set; }
}