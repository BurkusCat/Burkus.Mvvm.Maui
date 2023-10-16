using DemoApp.Models;
using DemoApp.ViewModels;
using DemoApp.Views;

namespace DemoApp.UnitTests.ViewModels;

public class FlyoutMenuViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public FlyoutMenuViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public FlyoutMenuViewModel ViewModel => new FlyoutMenuViewModel(
        mockNavigationService);

    [Fact]
    public void Constructor_WhenInitialized_SetsMenuPageList()
    {
        // Arrange
        // Act
        var viewModel = ViewModel;

        // Assert
        Assert.Equal(4, viewModel.MenuPages.Count);
    }

    [Fact]
    public void SwitchFlyoutDetailPageCommand_WhenCalled_SwitchesToSelectedDetailPage()
    {
        // Arrange
        var viewModel = ViewModel;
        var selectedItem = new FlyoutPageItem
        {
            Title = "MockItem",
            IconSource = "test.png",
            TargetType = typeof(TodoPage),
        };

        // Act
        viewModel.SwitchFlyoutDetailPageCommand.Execute(selectedItem);

        // Assert
        mockNavigationService.Received().SwitchFlyoutDetail(typeof(TodoPage));
    }
}