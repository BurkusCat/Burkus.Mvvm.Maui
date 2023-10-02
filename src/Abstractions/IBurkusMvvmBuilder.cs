namespace Burkus.Mvvm.Maui;

internal interface IBurkusMvvmBuilder
{
    Func<INavigationService, Task> onStartFunc { get; set; }
}
