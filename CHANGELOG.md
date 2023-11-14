# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [0.2.0] - 2023-11-14

### Added
- `Burkus.Mvvm.Maui` now automatically imported globally
- Basic FlyoutPage support
- Advanced navigation method: `.GoBack()`
- Source generators for project setup
  - Initial app launch requirements now handled automatically
  - Native back button handled on pages (opt a page out by using `DisableBackButtonNavigator`)

### Changed
- TargetFramework is now `net8.0` (instead of `net7.0`)

## [0.1.0] - 2023-10-15

### Added
- Builder extensions and `BurkusMvvmBuilder`
- `ResolveBindingContext` markup extension
- Navigation parameters and reserved navigation parameters
- `INavigatingEvents`
- `INavigatedEvents`
- `NavigationService`
  - Basic abstractions of MAUI navigation methods such as: `.Push(...)`, `.Pop`, etc.
  - Modal and animation options for navigation
  - Convenient, advanced navigation methods such as: `.ReplaceTopPage(...)`, `.ResetStackAndPush(...)`, etc.
  - URI Navigation using the `.Navigate(...)` method (unstable behavior)
  - `NavigationUriBuilder`
- `DialogService` - abstractions over the native .NET MAUI alerts/pop-ups/prompts/action sheets
- `ServiceResolver`
- Initial documentation
- Demo app and unit tests