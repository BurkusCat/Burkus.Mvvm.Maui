![Burkus.Mvvm.Maui](art/BurkusMvvmMauiLogo.png)
#### Navigation - Parameter Passing - Lifecycle Events - Native Dialogs - Testability

# Burkus.Mvvm.Maui (experimental)
`Burkus.Mvvm.Maui` is an MVVM (Model–view–viewmodel) framework for .NET MAUI. The library has some key aims it wants to provide:
- Be lightweight and only provide the parts of MVVM that MAUI needs 👟
  - MAUI has dependency injection built-in now, `Burkus.Mvvm.Maui` takes advantage of this.
  - `CommunityToolkit.Mvvm` provides excellent: commanding, observable properties, source generating attributes, and fast messaging. `Burkus.Mvvm.Maui` does not compete with any of this and the idea is that you should pair both libraries together (or another library that does those things). This is not forced upon you, however.
  - MAUI [without Shell] needs: navigation, passing parameters, lifecycle events, and modals. `Burkus.Mvvm.Maui` wants to provide these things.
- Be unit testable 🧪
  - .NET MAUI itself is difficult to unit test outside the box and sometimes third-party MAUI libraries can be too.
  - You *should* be easily able to assert that when you press a button, that the command that fires navigates you to a particular page.
- Be easy to understand and setup 📄
  - The APIs and syntax should be easy to setup/understand
  - The library should be well documented (the current plan is to document the library in this README)
- Be dependable for the future 🔮
  - The library is OSS and released under the MIT license. Contributors do not need to sign a CLA.
  - Individuals and businesses can fork it if it ever doesn't meet their needs.

**⚠️ WARNING**: `Burkus.Mvvm.Maui` is currently an experimental library. The API will change frequently and there will be frequent backwards compatibility breaking changes. This library will be versioned as ["0.y.z"](https://semver.org/#spec-item-4) until a well-liked, stable API has been found. Only then would a version "1.y.z" and beyond be released.

# Documentation 📗
See the `DemoApp` in the `/samples` folder of this repository for a full example of this library in action. The [demo app](/samples/DemoApp/) has examples of different types of navigation, configuring the library, using lifecycle events, passing parameters, and showing native dialogs.

## Getting started
1. Install `Burkus.Mvvm.Maui` into your main MAUI project from NuGet: <https://www.nuget.org/packages/Burkus.Mvvm.Maui> [![NuGet](https://img.shields.io/nuget/v/Burkus.Mvvm.Maui.svg?label=NuGet)](https://www.nuget.org/packages/Burkus.Mvvm.Maui/)
2. In your shared project's `App.xaml.cs`, remove any line where `MainPage` is set to a `Page` or an `AppShell`. Make `App` inherit from `BurkusMvvmApplication`. You should be left with a simpler `App` class like this:
``` csharp
public partial class App : BurkusMvvmApplication
{
    public App()
    {
        InitializeComponent();
    }
}
```
3. Update `App.xaml` in your shared project to be a `burkus:BurkusMvvmApplication`.
``` xml
<?xml version="1.0" encoding="UTF-8" ?>
<burkus:BurkusMvvmApplication
    ...
    xmlns:burkus="http://burkus.co.uk">
    ...
</burkus:BurkusMvvmApplication>
```
4. In your `MauiProgram.cs` file, call `.UseBurkusMvvm()` in your builder creation e.g.:

```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UseBurkusMvvm(burkusMvvm =>
            {
                burkusMvvm.OnStart(async (navigationService) =>
                {
                    await navigationService.Push<LoginPage>();
                });
            })
            ...
```
5. **💡 RECOMMENDED**: This library pairs great with the amazing `CommunityToolkit.Mvvm`. Follow its [Getting started](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/#getting-started) guide to add it.

## Registering views, viewmodels, and services
A recommended way to register your views, viewmodels, and services is by creating extension methods in your `MauiProgram.cs` file.

``` csharp
public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
{
    mauiAppBuilder.Services.AddTransient<HomeViewModel>();
    mauiAppBuilder.Services.AddTransient<SettingsViewModel>();

    return mauiAppBuilder;
}

public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
{
    mauiAppBuilder.Services.AddTransient<HomePage>();
    mauiAppBuilder.Services.AddTransient<SettingsPage>();

    return mauiAppBuilder;
}

public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
{
    mauiAppBuilder.Services.AddSingleton<IWeatherService, WeatherService>();

    return mauiAppBuilder;
}
```

## Dependency injection
### View setup
In your `xaml` page, you need to use the `ResolveBindingContext` markup extension so that the correct viewmodel will be resolved for your view during navigation.
``` xml
<ContentPage
    ...
    xmlns:burkus="http://burkus.co.uk"
    xmlns:vm="clr-namespace:DemoApp.ViewModels"
    BindingContext="{burkus:ResolveBindingContext x:TypeArguments=vm:HomeViewModel}"
    ...>
```

[NOTE: You may get an error when using the above syntax that will go away when you actually run the app.](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/13)

Complete example (`x:DataType` has also been added for [improved performance and better auto-complete suggestions in XAML](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/compiled-bindings)):
``` xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="DemoApp.Views.HomePage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:burkus="http://burkus.co.uk"
    xmlns:vm="clr-namespace:DemoApp.ViewModels"
    x:DataType="vm:HomeViewModel"
    BindingContext="{burkus:ResolveBindingContext x:TypeArguments=vm:HomeViewModel}">
    ...
</ContentPage>
```

### Viewmodel setup
In your viewmodel's constructor, include references to any services you want to be automatically resolved. In the below example, `Burkus.Mvvm.Maui`'s `INavigationService` and an example service called `IExampleService` will be resolved when navigating to `HomeViewModel`.

``` csharp
public HomeViewModel(
    INavigationService navigationService,
    IExampleService exampleService)
{
    this.navigationService = navigationService;
    this.exampleService = exampleService;
}
```

### ServiceResolver (not recommended)
You can use the static class `ServiceResolver` to resolve services elsewhere in your application (for example, inside of converters and inside of `xaml.cs` files). You should use this **sparingly** as it will make your code less unit-testable.

Typed service resolution:
``` csharp
ServiceResolver.Resolve<IExampleService>();
```

Untyped service resolution:
``` csharp
ServiceResolver.Resolve(IExampleService);
```

## Navigation service
`INavigationService` is automatically registered by `.UseBurkusMvvm(...)`. You can use it to: push pages, pop pages, pop to the root page, replace the top page of the app, reset the navigation stack, switch tabs, and more.

This is a simple navigation example where we push a "`TestPage`" onto the navigation stack:
``` csharp
await navigationService.Push<TestPage>();
```

Almost all the methods offer an overload where you can pass `NavigationParameters navigationParameters`. These parameters can be received by the page you are navigating to by using the [Burkus MVVM lifecycle events](#lifecycle-events-and-passing-parameters) in your viewmodel.

Here is an example where we set three parameters in three different ways and pass them to the next page:
``` csharp
var navigationParameters = new NavigationParameters
{
    // 1. on NavigationParameters object creation, set as many keys as you wish
    { "username", Username },
};

// 2. append an additional custom parameter
navigationParameters.Add("selection", Selection);

// 3. reserved parameter with a special meaning in the Burkus MVVM library, it has a helper method to make setting it easier
navigationParameters.UseModalNavigation = true;

await navigationService.Push<TestPage>(navigationParameters);
```

See the [INavigationService interface in the repository](https://github.com/BurkusCat/Burkus.Mvvm.Maui/blob/main/src/Abstractions/INavigationService.cs) for all possible navigation method options.

## Choosing the start page of your app
It is possible to have a service that decides which page is most appropriate to navigate to. This service could decide to:
  - Navigate to the "Terms & Conditions" page if the user has not agreed to the latest terms yet
  - Navigate to the "Signup / Login" page if the user is logged out
  - Navigate to the "Home" page if the user has used the app before and doesn't need to do anything
```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UseBurkusMvvm(burkusMvvm =>
            {
                burkusMvvm.OnStart(async (navigationService) =>
                {
                    var appStartupService = ServiceResolver.Resolve<IAppStartupService>();
                    await appStartupService.NavigateToFirstPage();
                });
            })
            ...
```

## Lifecycle events and passing parameters
### INavigatedEvents
If your viewmodel inherits from this interface, the below events will trigger for it.
- `OnNavigatedTo(parameters)`
  - You can use this lifecycle event to retrieve parameters passed to this page
  - Is similar to MAUI's `Page`' [OnNavigatedTo](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.page.onnavigatedto) event.
  ``` csharp
  public async Task OnNavigatedTo(NavigationParameters parameters)
  {
      Username = parameters.GetValue<string>("username");
  }
  ```

- `OnNavigatedFrom(parameters)`
  - Allows the page you are leaving to add additional parameters to the page you are navigating to
  - Is similar to MAUI's `Page`'s [OnNavigatedFrom](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.page.onnavigatedfrom) event.
  ``` csharp
  public async Task OnNavigatedFrom(NavigationParameters parameters)
  {
      parameters.Add("username", username);   
  }
  ```

### INavigatingEvents
If your viewmodel inherits from this interface, the below events will trigger for it.
- `OnNavigatingFrom(parameters)`
  - Allows the page you are leaving to add additional parameters to the page you are navigating to
  - Is similar to MAUI's `Page`'s [OnNavigatingFrom](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.page.onnavigatingfrom) event.
  ``` csharp
  public async Task OnNavigatingFrom(NavigationParameters parameters)
  {
      parameters.Add("username", username);   
  }
  ```
### Reserved navigation parameters
Several parameter keys have been pre-defined and are using by the `Burkus.Mvvm.Maui` library to adjust how navigation is performed.

- `ReservedNavigationParameters.UseAnimatedNavigation`
  - Should an animation be used during the navigation?
  - Defaults to: `true`
- `ReservedNavigationParameters.UseModalNavigation`
  - Should the navigation be performed modally?
  - Defaults to: `false`

The `NavigationParameters` object exposes some handy properties `.UseAnimatedNavigation` and `.UseModalNavigation` so you can easily set or check the value of these properties.

## Dialog service
`IDialogService` is automatically registered by `.UseBurkusMvvm(...)`. It is a testable service that is an abstraction over [the MAUI alerts/pop-ups/prompts/action sheets](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/pop-ups).

Register the service in your viewmodel constructor:

``` csharp
public HomeViewModel(
    IDialogService dialogService,
    INavigationService navigationService)
{
    this.dialogService = dialogService;
    this.navigationService = navigationService;
}
```

This is a simple example of showing an error alert message with the `DialogService`:
``` csharp
dialogService.DisplayAlert(
    "Error",
    "You must enter a username.",
    "OK");
```

See the [IDialogService interface in the repository](https://github.com/BurkusCat/Burkus.Mvvm.Maui/blob/main/src/Abstractions/IDialogService.cs) for all the possible method options.

## Advanced / complexities
The below are some things of note that may help prevent issues from arising:
- When you inherit from `BurkusMvvmApplication`, the `MainPage` of the app will be automatically set to a `NavigationPage`. This means the first page you push can be a `ContentPage` rather than needing to push a `NavigationPage`. This may change in the future.

# Roadmap 🛣️
- [URL-based navigation](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/1)
- [View and viewmodel auto-registration](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/4)
- [Popup pages](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/2)
- [Nested viewmodels](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/5)
- [OnNavigatingTo()](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/6)
- [IPageVisibilityEvents](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/7)
- [...and more](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues)

[Create an issue](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/new/choose) to add your own suggestions.

![Green letters: M V V M laid out vertically](art/MvvmVertical.png)

# Contributing 💁‍♀️
Contributions are very welcome! Please see the [contributing guide](CONTRIBUTING.MD) to get started.

[![NuGet release](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/release-nuget.yml/badge.svg)](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/release-nuget.yml)
[![Build for CI](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci.yml/badge.svg)](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci.yml)
[![Build Demo App for CI](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci-demo-app.yml/badge.svg)](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci-demo-app.yml)

# License 🪪
The project is distributed under the [MIT license](LICENSE). Contributors do not need to sign a CLA.