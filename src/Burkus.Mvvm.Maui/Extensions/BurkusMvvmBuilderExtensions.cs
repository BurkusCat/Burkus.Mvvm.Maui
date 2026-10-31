namespace Burkus.Mvvm.Maui;

public static class BurkusMvvmBuilderExtensions
{
    /// <summary>
    /// Define where the app should go first when starting. You must navigate to a page when starting.
    /// </summary>
    /// <param name="builder">BurkusMvvmBuilder</param>
    /// <param name="onStartFunc">Function to perform when starting with access to <see cref="INavigationService"/> and <see cref="IServiceProvider"/></param>
    /// <returns>The builder</returns>
    public static BurkusMvvmBuilder OnStart(this BurkusMvvmBuilder builder, Func<INavigationService, IServiceProvider, Task> onStartFunc)
    {
        var internalBuilder = builder as InternalBurkusMvvmBuilder;

        if (internalBuilder != null)
        {
            internalBuilder.onStartFunc = onStartFunc;
        }

        return builder;
    }

    /// <summary>
    /// Define where the app should go first when starting. You must navigate to a page when starting.
    /// </summary>
    /// <param name="builder">BurkusMvvmBuilder</param>
    /// <param name="onStartFunc">Function to perform when starting with access to <see cref="INavigationService"/>.</param>
    /// <returns>The builder</returns>
    public static BurkusMvvmBuilder OnStart(this BurkusMvvmBuilder builder, Func<INavigationService, Task> onStartFunc)
    {
        return OnStart(builder, (nav, sp) => onStartFunc(nav));
    }

    /// <summary>
    /// Define where the app should go first when starting. You must navigate to a page when starting.
    /// </summary>
    /// <param name="builder">BurkusMvvmBuilder</param>
    /// <param name="onStartFunc">Function to perform when starting with access to <see cref="IServiceProvider"/>.</param>
    /// <returns>The builder</returns>
    public static BurkusMvvmBuilder OnStart(this BurkusMvvmBuilder builder, Func<IServiceProvider, Task> onStartFunc)
    {
        return OnStart(builder, (nav, sp) => onStartFunc(sp));
    }

    /// <summary>
    /// Automatically sets public writable properties on the ViewModel that have the same name as a navigation parameter key when navigating to a page.
    /// </summary>
    /// <param name="builder">BurkusMvvmBuilder</param>
    /// <returns>The builder</returns>
    public static BurkusMvvmBuilder AutoSetPropertiesOnNavigation(this BurkusMvvmBuilder builder)
    {
        return AutoSetPropertiesOnNavigation(builder, null);
    }

    /// <summary>
    /// Automatically sets public writable properties on the ViewModel when navigating to a page. Each navigation parameter key is converted to a property name with the mapper.
    /// </summary>
    /// <param name="builder">BurkusMvvmBuilder</param>
    /// <param name="propertyNameMapper">Converts a navigation parameter key to a property name, for example "test_param" to "TestParam". Keys are used as property names when null.</param>
    /// <returns>The builder</returns>
    public static BurkusMvvmBuilder AutoSetPropertiesOnNavigation(this BurkusMvvmBuilder builder, Func<string, string>? propertyNameMapper)
    {
        AutoSetPropertiesOptions.Enabled = true;
        AutoSetPropertiesOptions.PropertyNameMapper = propertyNameMapper;

        return builder;
    }
}
