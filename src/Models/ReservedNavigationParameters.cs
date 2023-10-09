namespace Burkus.Mvvm.Maui;

/// <summary>
/// A set of fixed parameters defined by the library for special-use cases.
/// </summary>
public static class ReservedNavigationParameters
{
    /// <summary>
    /// Pass true if the navigation should be completed modally.
    /// </summary>
    public const string UseModalNavigation = nameof(UseModalNavigation);

    /// <summary>
    /// Pass true if the navigation should be animated.
    /// </summary>
    public const string UseAnimatedNavigation = nameof(UseAnimatedNavigation);

    /// <summary>
    /// Pass the name of the type of page that should be the initial selected tab within a <see cref="TabbedPage"/>.
    /// </summary>
    public const string SelectTab = nameof(SelectTab);
}
