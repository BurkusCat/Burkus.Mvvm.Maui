using DemoApp.ViewModels;

namespace DemoApp.UnitTests.ViewModels;

public class PageVisibilityViewModelTests
{
    private readonly INavigationService mockNavigationService = Substitute.For<INavigationService>();

    public PageVisibilityViewModelTests()
    {
    }

    public PageVisibilityEventViewModel ViewModel => new PageVisibilityEventViewModel(
        mockNavigationService);

    [Fact]
    public void Constructor_WhenResolved_ShouldSetNoProperties()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        // Assert
        Assert.False(viewModel.ShowLabel);
    }

    [Fact]
    public void OnAppearing_WhenShowLabelFalse_SetsShowLabelToTrue()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.OnAppearing();

        // Assert
        Assert.True(viewModel.ShowLabel);
    }

    [Fact]
    public void OnDisappearing_WhenShowLabelTrue_SetsShowLabelToFalse()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.ShowLabel = true;

        // Act
        viewModel.OnDisappearing();

        // Assert
        Assert.False(viewModel.ShowLabel);
    }
}