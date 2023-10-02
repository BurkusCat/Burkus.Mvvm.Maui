# Reporting bugs
[Use this template to create a bug report.](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/new?assignees=&labels=bug&projects=&template=bug_report.md&title=%5BBug%5D+)

# Suggesting features
[Use this template to create a suggest features or enhancements.](https://github.com/BurkusCat/Burkus.Mvvm.Maui/issues/new?assignees=&labels=enhancement&projects=&template=feature_request.md&title=%5BFeature%5D+) Remember the project/community may decide against a feature if it isn't a good fit for this library.

# Contributing pull requests
## Before coding anything...
If an issue doesn't already exist, it is probably a good idea to raise an issue on the GitHub for the bug/feature suggestion (see above). If you were to spend time making changes without doing this, then your pull request may not be accepted if the change is not wanted or it is implemented in a way that the project does not want. Especially for more significant changes, it is always better to discuss a proposed implementation on an issue first.

## Pre-requisites
- [Visual Studio 2022 - 17.7.0 or greater](https://visualstudio.microsoft.com/vs/community/) with the .NET Multi-platform App UI workload installed
  - [XAML Styler](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler2022) for ensuring XAML is formatted consistently. It is a good idea to set it to run automatically on save.

NOTE: The above software is the recommended basics. You may be able to use other software versions or different pieces of software to contribute to this project.

## Folder structure

- `/.github` - files related to the repository hosted on GitHub
- `/art` - art assets for the library
- `/samples` - full .NET MAUI applications demonstrating the features of the library
- `/src` - the source for the library
  - `/Abstractions` - contains interface files for services etc.
  - `/Builders` - builder classes for configuring the library
  - `/Extensions` - contains extension methods
  - `/Models` - models and constants
  - `/Properties` - contains some library configuration and string resources
  - `/Services` - dependencies and services for the project including publicly exposed ones
  - `/Utilities` - internal helpers for the library
- `/tests` - the area for test projects for the library
  - `/Burkus.Mvvm.Maui.UnitTests` - unit tests for the library project

  ## Submitting a PR
  - Try to rebase (`git rebase -i`) your PR onto `main` to tidy your git history https://www.atlassian.com/git/tutorials/rewriting-history/git-rebase
  - Update the unit tests (fix any existing tests and ensure new code has unit test coverage)
  - Ensure you have the [XAML Styler](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler2022) extension installed and have run it on any XAML files you have changed to format them