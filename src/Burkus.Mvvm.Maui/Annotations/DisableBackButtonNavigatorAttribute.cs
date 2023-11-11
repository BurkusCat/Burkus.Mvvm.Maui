namespace Burkus.Mvvm.Maui;

/// <summary>
/// Any <see cref="ContentPage"/>, <see cref="FlyoutPage"/>, <see cref="NavigationPage"/>,
/// or <see cref="TabbedPage"/> marked with this attribute will be excluded from having
/// <see cref="Page.OnBackButtonPressed"/> being overrided by <see cref="Burkus.Mvvm.Maui"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DisableBackButtonNavigatorAttribute : Attribute
{
}