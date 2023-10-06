using DemoApp.ViewModels;

namespace DemoApp.UnitTests.Services;

public class RegisterViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public RegisterViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public RegisterViewModel ViewModel => new RegisterViewModel(
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
}