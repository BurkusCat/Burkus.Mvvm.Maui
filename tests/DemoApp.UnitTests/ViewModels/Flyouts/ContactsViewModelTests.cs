using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.ViewModels;

public class ContactsViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public ContactsViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public ContactsViewModel ViewModel => new ContactsViewModel(
        mockNavigationService);

    [Fact]
    public void Constructor_WhenInitialized_SetsContactList()
    {
        // Arrange
        // Act
        var viewModel = ViewModel;

        // Assert
        Assert.Equal(6, viewModel.ContactNames.Count);
    }

    [Fact]
    public void GoToTabbedPageDemoCommand_WhenCalled_NavigatesToTabbedPage()
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.GoToTabbedPageDemoCommand.Execute(null);

        // Assert
        mockNavigationService.Received().Push<DemoTabsPage>();
    }
}