![Burkus.Mvvm.Maui](https://raw.githubusercontent.com/BurkusCat/Burkus.Mvvm.Maui/main/art/BurkusMvvmMauiLogo.png)
#### Navigation - Parameter Passing - Lifecycle Events - Native Dialogs - Testability

### Stable: [![NuGet Stable](https://img.shields.io/nuget/v/Burkus.Mvvm.Maui.svg?label=NuGet)](https://www.nuget.org/packages/Burkus.Mvvm.Maui/) [![Nuget](https://img.shields.io/nuget/dt/Burkus.Mvvm.Maui)](https://www.nuget.org/packages/Burkus.Mvvm.Maui) Latest Preview: [![NuGet Preview](https://img.shields.io/nuget/vpre/Burkus.Mvvm.Maui.svg?label=NuGet)](https://www.nuget.org/packages/Burkus.Mvvm.Maui/) 

# Burkus.Mvvm.Maui (experimental)
`Burkus.Mvvm.Maui` is an MVVM (Model–view–viewmodel) framework designed for .NET MAUI. The library is developed with these key principles:
- Be **lightweight** and only provide the parts of MVVM that MAUI needs 👟
  - MAUI has dependency injection built-in now, `Burkus.Mvvm.Maui` takes advantage of this.
  - `CommunityToolkit.Mvvm` provides excellent: commanding, observable properties, source generating attributes, and fast messaging. `Burkus.Mvvm.Maui` does not compete with any of this and the idea is that you should pair both libraries together (or another library that does those things). This is not forced upon you, however.
  - MAUI [without Shell] needs: navigation, passing parameters, lifecycle events, and dialogs. `Burkus.Mvvm.Maui` provides these things.
- Be **unit testable** 🧪
  - This library and its APIs are designed to ensure you can easily include `Burkus.Mvvm.Maui` calls in unit tests.
  - For example, you can confidently validate that button clicks lead to specific page navigations.
- Be **easy to understand** and setup 📄
  - The APIs and syntax are easy to setup & understand.
  - We are committed to providing comprehensive documentation this `README`.
- Be **dependable** for the future 🔮
  - `Burkus.Mvvm.Maui` is open source and released under the MIT license. No CLAs are required for contributors.
  - Individuals and businesses can fork the library if it ever falls short of their needs.

**⚠️ Warning**: `Burkus.Mvvm.Maui` is currently an experimental library. Expect frequent breaking API changes. This library will be versioned as ["0.y.z"](https://semver.org/#spec-item-4) until we establish a stable, well-liked API. Only then will we release versions "1.y.z" and beyond.

# Supporting the Project 💖
<a href="https://github.com/sponsors/BurkusCat"><img align="right" src="https://raw.githubusercontent.com/BurkusCat/Burkus.Mvvm.Maui/main/art/mona.png" alt="Mona the GitHub Sponsor Octocat smiling and holding a heart"></a>

Hi there 👋 I'm Ronan Burke aka Burkus. I maintain this project during my spare time and I would love to be able to dedicate more time each month to supporting it!  If you've found value in Burkus.Mvvm.Maui, I would greatly appreciate if you would be able to **[sponsor me on GitHub Sponsors](https://github.com/sponsors/BurkusCat)**. There are different rewards for each of the monthly or one-time sponsorship tiers such as:  

- a sponsorship badge 🪙
- prioritized bug reports 🐛
- opportunities for pair-programming sessions, consulting, or mentorship 🧑‍🏫
- shout-outs right here in this `README` 📢
- ... and more

<br clear="right"/>

⭐ If you like the project, please consider giving it a __GitHub Star__ ⭐

# Documentation 📗
See the `DemoApp` in the `/samples` folder of this repository for a full example of this library in action.

🚀 [Run the Demo App](/samples/DemoApp/) to see interactive examples of features in this library. With the code examples you can learn about:
- different types of navigation
- the standard way to configure this library
- utilizing lifecycle events
- passing parameters
- displaying native dialogs

🧪 [Check out the Test Project](/tests/DemoApp.UnitTests/) for demonstrations how you can write tests for code that calls this library. This will help ensure you write rock-solid apps!

| <img src="https://raw.githubusercontent.com/BurkusCat/Burkus.Mvvm.Maui/main/art/winui-login.png" width="612" alt="The Login page of the demo app running on WinUI"> | <img src="https://raw.githubusercontent.com/BurkusCat/Burkus.Mvvm.Maui/main/art/android-home.png" width="200" alt="The Home page of the demo app running on Android"> | <img src="https://raw.githubusercontent.com/BurkusCat/Burkus.Mvvm.Maui/main/art/android-tabs.png" width="200" alt="The Tabs page of the demo app running on Android"> |
| -------- | ------- | ------- |

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
                burkusMvvm.OnStart(async (INavigationService navigationService) =>
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
`INavigationService` is automatically registered by `.UseBurkusMvvm(...)`. You can use it to: push pages, pop pages, pop to the root page, go back, replace the top page of the app, reset the navigation stack, switch tabs, and more. 
See the [INavigationService interface in the repository](https://github.com/BurkusCat/Burkus.Mvvm.Maui/blob/main/src/Abstractions/INavigationService.cs) for all possible navigation method options.

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

// 2. append an additional, custom parameter
navigationParameters.Add("selection", Selection);

// 3. reserved parameter with a special meaning in the Burkus MVVM library, it has a helper method to make setting it easier
navigationParameters.UseModalNavigation = true;

await navigationService.Push<TestPage>(navigationParameters);
```

The `INavigationService` supports URI/URL-based navigation. Use the `.Navigate(string uri)` or `.Navigate(string uri, NavigationParameters navigationParameters)` methods to do more complex navigation.

**⚠️ Warning**: URI-based navigation behavior is unstable and is likely to change in future releases. Passing parameters, events triggered etc. are all inconsistent at present.

Here are some examples of URI navigation:
``` csharp
// use absolute navigation (starts with a "/") to go to the LoginPage
navigationService.Navigate("/LoginPage");

// push multiple pages using relative navigation onto the stack
navigationService.Navigate("AlphaPage/BetaPage/CharliePage");

// push a page relatively with query parameters
navigationService.Navigate("HomePage?username=Ronan&loggedIn=True");

// push a page with query parameters *and* navigation parameters
// - the query parameters only apply to one segment
// - the navigation parameters apply to the entire navigation
// - query parameters override navigation parameters
var parameters = new NavigationParameters { "example", 456 };
navigationService.Navigate("ProductPage?productid=123", parameters);

// go back one page modally
var parameters = new NavigationParameters();
parameters.UseModalNavigation = true;
navigationService.Navigate("..", parameters);

// go back three pages and push one new page
navigationService.Navigate("../../../AlphaPage");

// it is good practice to use nameof(x) to provide a compile-time reference to the pages in your navigation
navigationService.Navigate($"{nameof(YankeePage)}/{nameof(ZuluPage)}");
```
### Navigation URI builder
Navigation to multiple pages simultaneously and passing parameters to them can start to get complicated quickly. The `NavigationUriBuilder` is a simple, typed way to build a complex navigation string.

Below is an example where we go back a page (and pass a parameter that instructs the navigation to be performed modally), then push a `VictorPage`, and then push a `YankeePage` modally onto the stack:
``` csharp
var parameters = new NavigationParameters();
parameters.UseModalNavigation = true;

var navigationUri = new NavigationUriBuilder()
    .AddGoBackSegment(parameters)
    .AddSegment<VictorPage>()
    .AddSegment<YankeePage>(parameters)
    .Build() // produces the string: "..?UseModalNavigation=True/VictorPage/YankeePage/"

navigationService.Navigate(navigationUri);
```

## Choosing the start page of your app
In the [Getting Started guide](#getting-started), it shows how you can use `INavigationService` in `.OnStart(...)` to choose a single start page. To accomplish more complicated startup navigation scenarios, you can use the below `.OnStart(...)` overloads.

### (navigationService, serviceProvider)

In the below example, we use both an `INavigationService` and an `IServiceProvider`. The `IServiceProvider` is used to resolve the .NET MAUI service, [`IPreferences`](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/preferences?tabs=android). If a username is stored in preferences, we use the `INavigationService` to go to the `HomePage` of the app. Otherwise, we go to the `LoginPage`.

``` csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBurkusMvvm(burkusMvvm =>
            {
                burkusMvvm.OnStart(async (navigationService, serviceProvider) =>
                {
                    var preferences = serviceProvider.GetRequiredService<IPreferences>();

                    if (preferences.ContainsKey(PreferenceKeys.Username))
                    {
                        // we are logged in to the app
                        await navigationService.Push<HomePage>();
                    }
                    else
                    {
                        // logged out so we need to get the user to login
                        await navigationService.Push<LoginPage>();
                    }
                });
            })
            ...
```
### (IServiceProvider serviceProvider)

It is possible to have a service that decides which page is most appropriate to navigate to. This service could decide to:
  - Navigate to the "Terms & Conditions" page if the user has not agreed to the latest terms yet
  - Navigate to the "Signup / Login" page if the user is logged out
  - Navigate to the "Home" page if the user has used the app before and doesn't need to do anything

In the below example, we only resolve a `IServiceProvider` which allows us to resolve `IAppStartupService`. The `IAppStartupService` will call the `INavigationService` internally to do the navigation.
```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UseBurkusMvvm(burkusMvvm =>
            {
                burkusMvvm.OnStart(async (IServiceProvider serviceProvider) =>
                {
                    var appStartupService = serviceProvider.GetRequiredService<IAppStartupService>();
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
  - If true, uses an animation during navigation.
  - Type: `bool`
  - Default: `true`
- `ReservedNavigationParameters.UseModalNavigation`
  - If true, performs the navigation modally.
  - Type: `bool`
  - Default: `false`
- `ReservedNavigationParameters.SelectTab` 
  - If navigating to a `TabbedPage`, selects the tab with the name of the type passed.
  - Type: `string`
  - Default: `null`

The `NavigationParameters` object exposes some handy properties `.UseAnimatedNavigation` and `.UseModalNavigation` so you can easily set or check the value of these properties.

### Handling back button presses
By default, back button presses on Android/Windows will bypass `Burkus.Mvvm.Maui` which would mean lifecycle events and parameter passing may not happen when you expect them to. You can allow `Burkus.Mvvm.Maui` to handle the back button navigation for a page by turning it into a `Burkus...Page`. For example, below a `ContentPage` is turned into a `BurkusContentPage`.

``` xml
<burkus:BurkusContentPage
    ...
    xmlns:burkus="http://burkus.co.uk"
    ...>
```

``` csharp
public partial class HomePage : BurkusContentPage
```

The page types available are:
- `BurkusContentPage`
- `BurkusNavigationPage`
- `BurkusTabbedPage`
- `BurkusFlyoutPage`

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
- Adding this package to a project will automatically import the `Burkus.Mvvm.Maui` namespace globally if you have [`ImplicitUsings`](https://devblogs.microsoft.com/dotnet/welcome-to-csharp-10/#implicit-usings) enabled in your project. You can opt out of this by including the following in your `.csproj` file:
``` xml
<Using Remove="Burkus.Mvvm.Maui" />
```

# Roadmap 🛣️
- [View and viewmodel auto-registration](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/4)
- [Popup pages](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/2)
- [Nested viewmodels](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/5)
- [OnNavigatingTo()](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/6)
- [IPageVisibilityEvents](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/7)
- [Navigation Guards](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/28)
- [...and more](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues)

[Create an issue](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/new/choose) to add your own suggestions. Or, **support the project** and help influence its direction by [sponsoring me](https://github.com/sponsors/BurkusCat).

# Contributing 💁‍♀️
Contributions are very welcome! Please see the [contributing guide](CONTRIBUTING.MD) to get started.

[![NuGet release](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/release-nuget.yml/badge.svg)](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/release-nuget.yml)
[![Build for CI](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci.yml/badge.svg)](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci.yml)
[![Build Demo App for CI](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci-demo-app.yml/badge.svg)](https://github.com/BurkusCat/Burkus.Mvvm.Maui/actions/workflows/ci-demo-app.yml)

# License 🪪
The project is distributed under the [MIT license](LICENSE). Contributors do not need to sign a CLA.

![Green letters: M V V M laid out vertically](https://raw.githubusercontent.com/BurkusCat/Burkus.Mvvm.Maui/main/art/MvvmVertical.png)