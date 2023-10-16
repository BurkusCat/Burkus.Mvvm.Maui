using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.ViewModels;

public class LoginViewModelTests
{
    private readonly IDialogService mockDialogService;
    private readonly INavigationService mockNavigationService;
    private readonly IPreferences mockPreferences;

    public LoginViewModelTests()
    {
        mockDialogService = Substitute.For<IDialogService>();
        mockNavigationService = Substitute.For<INavigationService>();
        mockPreferences = Substitute.For<IPreferences>();
    }

    public LoginViewModel ViewModel => new LoginViewModel(
        mockDialogService,
        mockNavigationService,
        mockPreferences);

    [Fact]
    public void Constructor_WhenResolved_ShouldSetNoProperties()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        // Assert
        Assert.Null(viewModel.Username);
        Assert.Null(viewModel.Password);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void LoginCommand_WhenMissingUsername_ShouldShowErrorMessage(
        string username)
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.Username = username;
        viewModel.Password = "secure123";

        // Act
        viewModel.LoginCommand.Execute(null);

        // Assert
        mockPreferences.DidNotReceiveWithAnyArgs();
        mockDialogService.Received().DisplayAlert(
            "Error",
            "You must enter a username.",
            "OK");
        mockNavigationService.DidNotReceiveWithAnyArgs();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void LoginCommand_WhenMissingPassword_ShouldShowErrorMessage(
        string password)
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.Username = "burkus@test.com";
        viewModel.Password = password;

        // Act
        viewModel.LoginCommand.Execute(null);

        // Assert
        mockPreferences.DidNotReceiveWithAnyArgs();
        mockDialogService.Received().DisplayAlert(
            "Error",
            "You must enter a password.",
            "OK");
        mockNavigationService.DidNotReceiveWithAnyArgs();
    }

    [Theory]
    [InlineData("secure123")]
    [InlineData("   ")]
    public void LoginCommand_WhenValidLoginData_ShouldNavigateToHomePage(
        string password)
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.Username = "burkus@test.com";
        viewModel.Password = password;

        // Act
        viewModel.LoginCommand.Execute(null);

        // Assert
        mockPreferences.Received().Set("username", "burkus@test.com");
        mockNavigationService.Received().ResetStackAndPush<HomePage>(
            Arg.Is<NavigationParameters>(x => x.GetValue<string>("username") == "burkus@test.com"));
        mockDialogService.DidNotReceiveWithAnyArgs();
    }

    [Fact]
    public void RegisterCommand_WhenCalled_ShouldNavigateToRegisterPage()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.RegisterCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Push<RegisterPage>();
    }
}