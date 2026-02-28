using DemoApp.ViewModels;

namespace DemoApp.UnitTests.ViewModels;

public class MapPropertiesViewModelTests
{
    private readonly INavigationService mockNavigationService = Substitute.For<INavigationService>();

    public MapPropertiesViewModel ViewModel => new MapPropertiesViewModel(
        mockNavigationService);

    [Fact]
    public void Constructor_WhenResolved_ShouldSetNoProperties()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        // Assert
        Assert.False(viewModel.ShowLabel);
        Assert.Null(viewModel.LabelText);
    }

    [Fact]
    public async Task OnNavigatedTo_WhenNavigationParametersPassed_DoesNotSetProperties()
    {
        // Arrange
        var viewModel = ViewModel;
        var parameters = new NavigationParameters
        {
            { "showLabel", true },
            { "labelText", "Text I want to see" },
        };

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.False(viewModel.ShowLabel);
        Assert.Null(viewModel.LabelText);
    }
}