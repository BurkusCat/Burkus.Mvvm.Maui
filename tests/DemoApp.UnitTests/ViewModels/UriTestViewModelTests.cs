using DemoApp.ViewModels;

namespace DemoApp.UnitTests.Services;

public class UriTestViewModelTests
{
    private readonly INavigationService mockNavigationService;

    public UriTestViewModelTests()
    {
        mockNavigationService = Substitute.For<INavigationService>();
    }

    public UriTestViewModel ViewModel => new UriTestViewModel(
        mockNavigationService);

    [Theory]
    [InlineData(1, "../")]
    [InlineData(2, "../../")]
    [InlineData(3, "../../../")]
    public void GoBackMultipleTimesCommand_WhenCalled_NavigatesMultiplePagesBack(
        int backTimes,
        string navigationString)
    {
        // Arrange
        var viewModel = ViewModel;

        // Act
        viewModel.GoBackMultipleTimesCommand.Execute(backTimes);

        // Assert
        mockNavigationService.Received().Navigate(navigationString);
    }
}