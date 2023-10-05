using DemoApp.Abstractions;
using DemoApp.Services;
using DemoApp.ViewModels;
using DemoApp.Views;
using Microsoft.Extensions.Logging;

namespace DemoApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBurkusMvvm(burkusMvvm =>
            {
                burkusMvvm.OnStart(async (navigationService) =>
                {
                    await navigationService.Push<LoginPage>();
                });
            })
            .RegisterViewModels()
            .RegisterViews()
            .RegisterServices()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<ChangeUsernameViewModel>();
        mauiAppBuilder.Services.AddTransient<DemoTabsViewModel>();
        mauiAppBuilder.Services.AddTransient<HomeViewModel>();
        mauiAppBuilder.Services.AddTransient<LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<RegisterViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<ChangeUsernamePage>();
        mauiAppBuilder.Services.AddTransient<DemoTabsPage>();
        mauiAppBuilder.Services.AddTransient<HomePage>();
        mauiAppBuilder.Services.AddTransient<LoginPage>();
        mauiAppBuilder.Services.AddTransient<RegisterPage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IWeatherService, WeatherService>();

        return mauiAppBuilder;
    }
}