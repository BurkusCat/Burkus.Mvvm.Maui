using DemoApp.Abstractions;
using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.Services;

public class HomeViewModelTests
{
    private readonly INavigationService mockNavigationService;
    private readonly IWeatherService mockWeatherService;
    private readonly IPreferences mockPreferences;

    public HomeViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
        mockPreferences = Substitute.For<IPreferences>();
        mockWeatherService = Substitute.For<IWeatherService>();
    }

    public HomeViewModel ViewModel => new HomeViewModel(
        mockNavigationService,
        mockPreferences,
        mockWeatherService);

    [Fact]
    public void Constructor_WhenResolved_ShouldSetNoProperties()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        // Assert
        Assert.Null(viewModel.Username);
        Assert.Null(viewModel.CurrentWeatherDescription);
    }

    [Fact]
    public async Task OnNavigatedTo_WhenUsernamePassed_SetsUsernameValue()
    {
        // Arrange
        var viewModel = ViewModel;
        var parameters = new NavigationParameters
        {
            { "username", "Ronan" },
        };
        mockWeatherService.GetWeatherDescription().Returns("It's while coul so it is");

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.Equal("Ronan", viewModel.Username);
        Assert.Equal("It's while coul so it is", viewModel.CurrentWeatherDescription);

        mockWeatherService.Received().GetWeatherDescription();
    }

    [Fact]
    public async Task OnNavigatedTo_WhenNoUsernamePassed_SetsUsernameFromPreferences()
    {
        // Arrange
        var viewModel = ViewModel;
        var parameters = new NavigationParameters();
        mockPreferences.Get<string>("username", null).Returns("Smee");

        // Act
        await viewModel.OnNavigatedTo(parameters);

        // Assert
        Assert.Equal("Smee", viewModel.Username);
    }

    [Fact]
    public void ChangeUsernameWithAnimationCommand_WhenCalled_NavigatesToChangeUsernamePageWithAnimation()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.ChangeUsernameWithAnimationCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Push<ChangeUsernamePage>(
            Arg.Is<NavigationParameters>(x => x.GetValue<bool>("UseModalNavigation") == true
                && !x.ContainsKey("UseAnimatedNavigation")));
    }

    [Fact]
    public void GoToTabbedPageDemoCommand_WhenCalled_NavigatesToDemoTabsPage()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.GoToTabbedPageDemoCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Push<DemoTabsPage>();
    }

    [Fact]
    public void LogoutCommand_WhenCalled_NavigatesToLoginPage()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.LogoutCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Navigate("/LoginPage");
        mockPreferences.Received().Remove("username");
    }

    [Fact]
    public void AddMultiplePagesCommand_WhenCalled_NavigatesToSeveralPages()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.AddMultiplePagesCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Navigate("DemoTabsPage/RegisterPage/UriTestPage");
    }

    [Fact]
    public void ExitCommand_WhenCalled_ClosesTheApp()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.ExitCommand.Execute(null);

        // Assert
        mockNavigationService.Received().GoBack();
    }
}