using DemoApp.Abstractions;
using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.Services;

public class ChangeUsernameViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public ChangeUsernameViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public ChangeUsernameViewModel ViewModel => new ChangeUsernameViewModel(
        mockNavigationService);

    [Fact]
    public void Constructor_WhenResolved_ShouldSetNoProperties()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        // Assert
        Assert.Null(viewModel.Username);
    }

    [Fact]
    public async Task OnNavigatingFrom_WhenAnimationNotUsed_ShouldAddNavigationParameters()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.Username = "Burkus";
        var parameters = new NavigationParameters();

        // Act
        await viewModel.OnNavigatingFrom(parameters);

        // Assert
        Assert.Equal("Burkus", parameters.GetValue<string>("username"));
        Assert.True(parameters.GetValue<bool>("UseModalNavigation"));
        Assert.False(parameters.GetValue<bool>("UseAnimatedNavigation"));
    }

    [Fact]
    public async Task OnNavigatingFrom_WhenAnimationUsed_ShouldAddNavigationParameters()
    {
        // Arrange
        var viewModel = ViewModel;

        var navigatingToParameters = new NavigationParameters();
        navigatingToParameters.UseAnimatedNavigation = true;
        await viewModel.OnNavigatedTo(navigatingToParameters);

        viewModel.Username = "Burkus";
        var navigatingFromParameters = new NavigationParameters();

        // Act
        await viewModel.OnNavigatingFrom(navigatingFromParameters);

        // Assert
        Assert.Equal("Burkus", navigatingFromParameters.GetValue<string>("username"));
        Assert.True(navigatingFromParameters.GetValue<bool>("UseModalNavigation"));
        Assert.True(navigatingFromParameters.GetValue<bool>("UseAnimatedNavigation"));
    }

    [Fact]
    public void FinishCommand_WhenCalled_NavigatesBackToHomePage()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.FinishCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Pop();
    }
}