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
}
