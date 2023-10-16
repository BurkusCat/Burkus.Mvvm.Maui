using DemoApp.ViewModels;

namespace DemoApp.UnitTests.ViewModels;

public class DemoFlyoutViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public DemoFlyoutViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public DemoFlyoutViewModel ViewModel => new DemoFlyoutViewModel(
        mockNavigationService);
}