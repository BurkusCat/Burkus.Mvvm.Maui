namespace Burkus.Mvvm.Maui;

internal interface IBurkusMvvmBuilder
{
    Func<INavigationService, IServiceProvider, Task> onStartFunc { get; set; }
}
