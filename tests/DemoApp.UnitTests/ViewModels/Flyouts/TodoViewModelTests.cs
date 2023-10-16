using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.ViewModels;

public class TodoViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public TodoViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public TodoViewModel ViewModel => new TodoViewModel(
        mockNavigationService);

    [Fact]
    public void PushNewPageToFlyoutDetailCommand_WhenCalled_DoesNothing()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.PushNewPageToFlyoutDetailCommand.Execute(null);

        // Assert
        mockNavigationService.DidNotReceiveWithAnyArgs();
    }
}