using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.Services;

public class DemoTabsViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public DemoTabsViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public DemoTabsViewModel ViewModel => new DemoTabsViewModel(
        mockNavigationService);

    [Fact]
    public void GoBackCommand_WhenCalled_NavigatesToPreviousPage()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.GoBackCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Pop();
    }

    [Fact]
    public void SwitchToAlphaTabPageCommand_WhenCalled_SelectsAlphaTab()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.SwitchToAlphaTabPageCommand.Execute(null);

        // Assert
        mockNavigationService.Received().SelectTab<AlphaTabPage>();
    }

    [Fact]
    public void SwitchToBetaTabPageCommand_WhenCalled_SelectsBetaTab()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.SwitchToBetaTabPageCommand.Execute(null);

        // Assert
        mockNavigationService.Received().SelectTab<BetaTabPage>();
    }

    [Fact]
    public void SwitchToCharlieTabPageCommand_WhenCalled_SelectsCharlieTab()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.SwitchToCharlieTabPageCommand.Execute(null);

        // Assert
        mockNavigationService.Received().SelectTab<CharlieTabPage>();
    }
}