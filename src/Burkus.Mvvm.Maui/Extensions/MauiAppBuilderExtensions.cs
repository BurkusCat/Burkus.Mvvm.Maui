using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Burkus.Mvvm.Maui;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseBurkusMvvm(this MauiAppBuilder mauiAppBuilder, Action<BurkusMvvmBuilder> burkusMvvmBuilderAction)
    {
        mauiAppBuilder.Services.TryAddEnumerable(
            ServiceDescriptor.Transient<IMauiInitializeService, BurkusMvvmMauiInitializer>());
        mauiAppBuilder.Services.TryAddEnumerable(
            ServiceDescriptor.Transient<INavigationService, NavigationService>());
        mauiAppBuilder.Services.TryAddEnumerable(
            ServiceDescriptor.Transient<IDialogService, DialogService>());

        var burkusMvvmBuilder = new InternalBurkusMvvmBuilder();
        burkusMvvmBuilderAction(burkusMvvmBuilder);

        mauiAppBuilder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<IBurkusMvvmBuilder>(burkusMvvmBuilder));

        return mauiAppBuilder;
    }
}
