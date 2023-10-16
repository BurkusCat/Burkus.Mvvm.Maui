using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.ViewModels;

public class RemindersViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public RemindersViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public RemindersViewModel ViewModel => new RemindersViewModel(
        mockNavigationService);

    [Fact]
    public void SwitchFlyoutDetailToContactsCommand_WhenCalled_SwitchesFlyoutDetailToContactsPage()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.SwitchFlyoutDetailToContactsCommand.Execute(null);

        // Assert
        mockNavigationService.Received().SwitchFlyoutDetail<ContactsPage>();
    }
}