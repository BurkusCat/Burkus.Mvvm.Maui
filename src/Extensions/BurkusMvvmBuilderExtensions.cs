namespace Burkus.Mvvm.Maui;

public static class BurkusMvvmBuilderExtensions
{
    /// <summary>
    /// Define where the app should go first when starting. You must navigate to a page when starting.
    /// </summary>
    /// <param name="builder">BurkusMvvmBuilder</param>
    /// <param name="onStartFunc">Function to perform when starting with access to the <see cref="INavigationService"/></param>
    /// <returns></returns>
    public static BurkusMvvmBuilder OnStart(this BurkusMvvmBuilder builder, Func<INavigationService, Task> onStartFunc)
    {
        var internalBuilder = builder as InternalBurkusMvvmBuilder;

        if (internalBuilder != null)
        {
            internalBuilder.onStartFunc = onStartFunc;
        }

        return builder;
    }
}
