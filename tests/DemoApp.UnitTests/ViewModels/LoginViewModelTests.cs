using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.Services;

public class LoginViewModelTests
{
    private readonly IDialogService mockDialogService;
    private readonly INavigationService mockNavigationService;

    public LoginViewModelTests()
    {
        mockDialogService = Substitute.For<IDialogService>();
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public LoginViewModel ViewModel => new LoginViewModel(
        mockDialogService,
        mockNavigationService);

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

    [Fact]
    public void LoginCommand_WhenMissingUsername_ShouldShowErrorMessage()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.Password = "secure123";

        // Act
        viewModel.LoginCommand.Execute(null);

        // Assert
        mockDialogService.Received().DisplayAlert(
            "Error",
            "You must enter a username.",
            "OK");
        mockNavigationService.DidNotReceive().Push<HomePage>();
    }

    [Fact]
    public void LoginCommand_WhenMissingPassword_ShouldShowErrorMessage()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.Username = "burkus@test.com";

        // Act
        viewModel.LoginCommand.Execute(null);

        // Assert
        mockDialogService.Received().DisplayAlert(
            "Error",
            "You must enter a password.",
            "OK");
        mockNavigationService.DidNotReceive().Push<HomePage>();
    }

    [Fact]
    public void LoginCommand_WhenValidLoginData_ShouldNavigateToHomePage()
    {
        // Arrange
        var viewModel = ViewModel;
        viewModel.Username = "burkus@test.com";
        viewModel.Password = "secure123";

        // Act
        viewModel.LoginCommand.Execute(null);

        // Assert
        mockNavigationService.Received().ResetStackAndPush<HomePage>(
            Arg.Is<NavigationParameters>(x => x.GetValue<string>("username") == "burkus@test.com"));
        mockDialogService.DidNotReceive().DisplayAlert(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>());
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